using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Get()
        {
            var teachers = await _userManager.GetUsersInRoleAsync("Teacher");

            var teacherDtos = teachers.Select(x => _mapper.Map(x, new UserGetDto()));

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

        // DELETE api/<TeacherController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var userToDelete = await _userManager.FindByIdAsync(id);

            if (userToDelete is null) return NotFound();

            await _userManager.RemoveFromRoleAsync(userToDelete, "Teacher");

            await _userManager.AddToRoleAsync(userToDelete, "User");

            return Ok(new { userToDelete.Name, userToDelete.Surname });
        }
    }
}
