using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniBook.DTOs.Student;
using UniBook.DTOs.Teacher;
using UniBook.Entities;

namespace UniBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Rector")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var teachers = await _userManager.GetUsersInRoleAsync("Teacher");

            var teacherDtos = teachers.Select(x => _mapper.Map(x, new UserGetDto()));

            var students = await _userManager.GetUsersInRoleAsync("Student");

            var studentDtos = students.Select(x => _mapper.Map(x, new UserGetDto()));

            return Ok(new {teacherDtos, studentDtos});
        }
    }
}
