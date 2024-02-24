using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniBook.AutoMapper;
using UniBook.Data;
using UniBook.DTOs.Student;
using UniBook.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UniBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public StudentController(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        // GET: api/<StudentController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var students = await _userManager.GetUsersInRoleAsync("Student");

            var studentDtos = students.Select(x => _mapper.Map(x, new UserGetDto()));

            return Ok(studentDtos);
        }

        // GET api/<StudentController>/id
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var studentInRole = await _userManager.GetUsersInRoleAsync("Student");

            if (studentInRole is null) return NotFound();

            var student = studentInRole.FirstOrDefault(x=>x.Id == id);

            var dto = new UserGetDto();

            _mapper.Map(student, dto);

            return Ok(dto);
        }

        // POST api/<StudentController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string id)
        {
            var userToAdd = await _userManager.FindByIdAsync(id);

            await _userManager.RemoveFromRoleAsync(userToAdd!, "User");

            string[] roles = { "Student", "Teacher", "Rector" };

            foreach (var role in roles)
            {
                var exsitingRole = await _userManager.IsInRoleAsync(userToAdd!, role);

                if(exsitingRole) return BadRequest();
            }

            if (userToAdd is null) return NotFound();

            await _userManager.AddToRoleAsync(userToAdd, "Student");

            return Ok(new { userToAdd.Name, userToAdd.Surname });
        }

        // DELETE api/<StudentController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var userToDelete = await _userManager.FindByIdAsync(id);

            if (userToDelete is null) return NotFound();

            await _userManager.RemoveFromRoleAsync(userToDelete, "Student");

            await _userManager.AddToRoleAsync(userToDelete, "User");

            return Ok(new {userToDelete.Name, userToDelete.Surname});
        }
    }
}
