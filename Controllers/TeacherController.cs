using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using UniBook.Data;
using UniBook.DTOs.Student;
using UniBook.DTOs.Teacher;
using UniBook.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UniBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public TeacherController(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        // GET: api/<TeacherController>
        [HttpGet]
        public async Task<IActionResult> Get([FromServices] AppDbContext context)
        {
            var teachers = await _userManager.GetUsersInRoleAsync("Teacher");

            foreach (var teacher in teachers)
            {
                await context.Entry(teacher).Reference(x => x.Subject).LoadAsync();
                await context.Entry(teacher).Collection(x=>x.UserGroups!).LoadAsync();
            }

            var teacherDtos = teachers.Select(x => _mapper.Map(x, new TeacherGetDto()));

            return Ok(teacherDtos);
        }

        // GET api/<teacherController>/id
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var teacherInRole = await _userManager.GetUsersInRoleAsync("Teacher");

            if (teacherInRole is null) return NotFound();

            var teacher = teacherInRole.FirstOrDefault(x => x.Id == id);

            var dto = new UserGetDto();

            _mapper.Map(teacher, dto);

            return Ok(dto);
        }

        // POST api/<TeacherController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string id)
        {
            var userToAdd = await _userManager.FindByIdAsync(id);

            await _userManager.RemoveFromRoleAsync(userToAdd!, "User");

            string[] roles = { "teacher", "Teacher", "Rector" };

            foreach (var role in roles)
            {
                var exsitingRole = await _userManager.IsInRoleAsync(userToAdd!, role);

                if (exsitingRole) return BadRequest();
            }

            if (userToAdd is null) return NotFound();

            await _userManager.AddToRoleAsync(userToAdd, "Teacher");

            return Ok(new {userToAdd.Name, userToAdd.Surname});
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Post(string id, [FromBody] TeacherSubjectPostDto dto, [FromServices] AppDbContext context)
        {
            if (!ModelState.IsValid) return BadRequest();

            var teacher = await context.Users.Include(u => u.Subject).FirstOrDefaultAsync(u => u.Id == id);

            if (teacher is null) return NotFound("Teacher not found.");

            var subject = await context.Subjects.FirstOrDefaultAsync(x => x.Id == dto.SubjectId);

            if (subject is null) return NotFound("Subject not found");

            teacher.Subject = subject;
            await context.SaveChangesAsync();

            return Ok($"Subject '{subject.Name}' added to teacher '{teacher.Name} {teacher.Surname}' successfully.");
        }

        //Put api/<TeacherContoller>

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] TeacherPutDto dto, [FromServices] AppDbContext context)
        {
            var teacher  = await context.Users.Include(x=>x.Subject).FirstOrDefaultAsync(x=>x.Id == id);

            if (teacher is null) return NotFound();

            var isTeacher = await _userManager.IsInRoleAsync(teacher!, "Teacher");

            if (!isTeacher) return BadRequest();
            
            if(teacher.SubjectId != dto.SubjectId)
            {
                var subject =  await context.Subjects.FirstOrDefaultAsync(x=>x.Id == dto.SubjectId);
                if (subject is null) return NotFound();
            }
            _mapper.Map(dto, teacher);

            await context.SaveChangesAsync();
            
            return Ok(teacher.Id);
        }

        // DELETE api/<TeacherController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id, [FromServices] AppDbContext context)
        {
            var userToDelete = await _userManager.FindByIdAsync(id);

            if (userToDelete is null) return NotFound();

            var existingUserGroup = await context.UserGroups.Where(x => x.UserId == id).ToListAsync();

            var teacherToDelete = await context.Users.Include(u => u.Subject).FirstOrDefaultAsync(u => u.Id == id);

            if (teacherToDelete is null) return NotFound();

            if (teacherToDelete.Subject != null)
            {
                teacherToDelete.Subject = null;
            }

            await context.SaveChangesAsync();

            if (existingUserGroup is not null)
            {
                foreach (var group in existingUserGroup)
                {
                    context.UserGroups.Remove(group);
                    await context.SaveChangesAsync();
                }
            }

            await _userManager.RemoveFromRoleAsync(userToDelete, "Teacher");

            await _userManager.AddToRoleAsync(userToDelete, "User");

            return Ok(new { userToDelete.Name, userToDelete.Surname });
        }
    }
}
