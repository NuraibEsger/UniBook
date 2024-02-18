using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniBook.Data;
using UniBook.DTOs.Department.cs;
using UniBook.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UniBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public DepartmentController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // GET: api/<DepartmentController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var departmentDto = await _context.Departments
                .Include(x=>x.Groups)
                .Select(x=>_mapper.Map(x, new DepartmentGetDto()))
                .AsNoTracking()
                .ToListAsync();

            return Ok(departmentDto);
        }

        // GET api/<DepartmentController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var department = await _context.Departments.Include(x=>x.Groups).FirstOrDefaultAsync(x=>x.Id == id);

            if(department is null) return NotFound();

            var dto = new DepartmentGetDto();

            _mapper.Map(department, dto);

            return Ok(dto);
        }

        // POST api/<DepartmentController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DepartmentPostDto dto)
        {
            if(!ModelState.IsValid) return BadRequest();

            var department = new Department();

            _mapper.Map(dto, department);

            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();

            return Ok(department.Id);
        }

        // PUT api/<DepartmentController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, DepartmentPutDto dto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var department = await _context.Departments.FirstOrDefaultAsync(x=>x.Id == id);

            if(department is null) return NotFound();

            _mapper.Map(dto, department);

            return Ok(department.Id);
        }

        // DELETE api/<DepartmentController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var department = await _context.Departments.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if(department is null) return NotFound();

            _context.Departments.Remove(department);
            
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
