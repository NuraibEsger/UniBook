using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniBook.Data;
using UniBook.DTOs.Department.cs;
using UniBook.DTOs.Student;
using UniBook.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UniBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public StudentController(IMapper mapper, AppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        // GET: api/<StudentController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var student = await _context.Students
                .Select(x => _mapper
                .Map(x, new StudentGetDto()))
                .AsNoTracking()
                .ToListAsync();

            return Ok(student);
        }

        // GET api/<StudentController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var student = await _context.Students.FirstOrDefaultAsync(x => x.Id == id);

            if (student is null) return NotFound();

            var dto = new StudentGetDto();

            _mapper.Map(student, dto);

            return Ok(dto);
        }

        // POST api/<StudentController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] StudentPostDto dto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var student = new Student();

            _mapper.Map(dto, student);

            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();

            return Ok(student.Id);

        }

        // PUT api/<StudentController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, DepartmentPutDto dto)
        {
            if(!ModelState.IsValid) return BadRequest();

            var student = await _context.Students.FirstOrDefaultAsync(x => x.Id == id);

            if (student is null) return BadRequest();

            _mapper.Map(dto, student);

            await _context.SaveChangesAsync();

            return Ok(student.Id);
        }

        // DELETE api/<StudentController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _context.Students.FirstOrDefaultAsync(x => x.Id == id);

            if (student is null) return BadRequest();

            _context.Students.Remove(student);

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
