using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniBook.Data;
using UniBook.DTOs.Department.cs;
using UniBook.DTOs.Group;
using UniBook.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UniBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public GroupController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // GET: api/<GroupController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var departmentDto = await _context.Groups
                .Include(x => x.Department)
                .Select(x => _mapper.Map(x, new GroupGetDto()))
                .AsNoTracking()
                .ToListAsync();

            return Ok(departmentDto);
        }

        // GET api/<GroupController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var group = await _context.Groups.Include(x=>x.Department).FirstOrDefaultAsync(x => x.Id == id);

            if (group is null) return NotFound();

            var dto = new GroupGetDto();

            _mapper.Map(group,dto);

            return Ok(dto);
        }

        // POST api/<GroupController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GroupPostDto dto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var department = await _context.Departments.FirstOrDefaultAsync(x => x.Id == dto.DepartmentId);

            if (department is null) return NotFound();

            var group = new Group();

            _mapper.Map(dto,group);

            await _context.Groups.AddAsync(group);
            await _context.SaveChangesAsync();

            return Ok(group.Id);
        }

        // PUT api/<GroupController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, GroupPutDto dto)
        {

            if (!ModelState.IsValid) return BadRequest();
            
            var group = await _context.Groups.Include(x=>x.Department).FirstOrDefaultAsync(x=>x.Id == id);
            if (group is null) return NotFound();

            if (group.DepartmentId != dto.DepartmentId)
            {
                var department = await _context.Departments.FirstOrDefaultAsync(x=>x.Id == dto.DepartmentId);
                if (department is null) return NotFound();
            }

            _mapper.Map(dto,group);

            await _context.SaveChangesAsync();

            return Ok(group.Id);
        }

        // DELETE api/<GroupController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var group = await _context.Groups.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (group is null) return NotFound();

            _context.Groups.Remove(group);

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
