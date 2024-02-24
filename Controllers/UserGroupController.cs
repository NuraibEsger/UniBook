using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniBook.Data;
using UniBook.DTOs.UserGroup;
using UniBook.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UniBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserGroupController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public UserGroupController(AppDbContext context, IMapper mapper, UserManager<AppUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }
        // GET: api/<UserGroupController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userGroups = await _context.UserGroups
                .Include(x=>x.Group)
                .Include(x=>x.User)
                .Select(x=> _mapper.Map(x, new UserGroupGetDto()))
                .ToListAsync();

            return Ok(userGroups);
        }

        // GET api/<UserGroupController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var userGroup = await _context.UserGroups
                .Include(x=>x.Group)
                .Include(x=>x.User)
                .FirstOrDefaultAsync(x=>x.Id == id);

            if (userGroup is null) return NotFound();

            var userGroupDto = new UserGroupGetDto();

            _mapper.Map(userGroup, userGroupDto);

            return Ok(userGroupDto);
        }

        // POST api/<UserGroupController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserGroupPostDto dto)
        {
            if (!ModelState.IsValid) return BadRequest("User not found.");

            var user = await _userManager.FindByIdAsync(dto.UserId!);
            if (user == null)
                return BadRequest("User not found.");

            var isStudent = await _userManager.IsInRoleAsync(user, "Student");

            var isTeacher = await _userManager.IsInRoleAsync(user, "Teacher");

            if(isStudent)
            {
                var existingUserGroup = await _context.UserGroups.FirstOrDefaultAsync(x=>x.UserId ==  dto.UserId);
                if (existingUserGroup != null) 
                    return BadRequest("The student already belongs to a group.");
            }

            var userGroup = new UserGroup();

            _mapper.Map(dto, userGroup);    

            await _context.UserGroups.AddAsync(userGroup);

            await _context.SaveChangesAsync();

            return Ok(userGroup.Id);
        }

        // PUT api/<UserGroupController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserGroupPutDto dto)
        {
            if (ModelState.IsValid) return BadRequest();

            var userGroup = await _context.UserGroups.FirstOrDefaultAsync(x=>x.Id == id);

            if (userGroup is null) return NotFound();

            var existingUserGroup = await _context.UserGroups.FirstOrDefaultAsync(x => x.GroupId == dto.GroupId);

            _mapper.Map(dto, userGroup);

            await _context.SaveChangesAsync();

            return Ok(userGroup.Id);
        }

        // DELETE api/<UserGroupController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userGroup = await _context.UserGroups.FirstOrDefaultAsync(x=>x.Id == id);
            if (userGroup is null) return BadRequest();

            _context.Remove(userGroup);
            await _context.SaveChangesAsync();

            return Ok(userGroup.Id);
        }
    }
}
    