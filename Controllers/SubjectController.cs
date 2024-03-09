using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniBook.Data;
using UniBook.DTOs.Department.cs;
using UniBook.DTOs.Subject;
using UniBook.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UniBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public SubjectController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // GET: api/<SubjectController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var subjectDto = await _context.Subjects
                .Select(x => _mapper.Map(x, new SubjectGetDto()))
                .AsNoTracking()
                .ToListAsync();

            return Ok(subjectDto);
        }

        // POST api/<SubjectController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SubjectPostDto dto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var subject = new Subject();

            _mapper.Map(dto, subject);

            await _context.Subjects.AddAsync(subject);
            await _context.SaveChangesAsync();

            return Ok(subject.Id);
        }

        // PUT api/<SubjectController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, SubjectPutDto dto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var subject = await _context.Subjects.FirstOrDefaultAsync(x => x.Id == id);

            if (subject is null) return NotFound();

            _mapper.Map(dto, subject);

            await _context.SaveChangesAsync();

            return Ok(subject.Id);
        }

        // DELETE api/<SubjectController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var subject = await _context.Subjects.Include(x=>x.Users).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (subject is null) return NotFound();

            foreach(var teacher in subject.Users!)
            {
                teacher.SubjectId = id;
            }

            _context.Subjects.Remove(subject);

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
