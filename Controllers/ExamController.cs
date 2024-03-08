using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniBook.Data;
using UniBook.DTOs.Exam;
using UniBook.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UniBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ExamController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // GET: api/<ExamController>
        [HttpGet]
        public async Task<IActionResult> Get(string id)
        {
            // Fetch the groups associated with the user
            var userGroups = await _context.UserGroups
                .Where(ug => ug.UserId == id)
                .Select(ug => ug.GroupId)
                .ToListAsync();

            // Fetch the exams related to the user's groups
            var examDto = await _context.Exams
                .Include(x=>x.Subject)
                .Include(x => x.Group)
                .Where(x => userGroups.Contains(x.GroupId))
                .Select(x => _mapper.Map(x, new ExamGetDto()))
                .AsNoTracking()
                .ToListAsync();

            return Ok(examDto); 
        }

        //GET api/<ExamController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var exam = await _context.Exams.Include(x=>x.GroupId).FirstOrDefaultAsync(x => x.GroupId == id);

            if (exam is null) return NotFound();

            var dto = new ExamGetDto();

            _mapper.Map(exam, dto);

            return Ok(dto);
        }

        // POST api/<ExamController>
        [HttpPost("{id}")]
        public async Task<IActionResult> Post(string id, [FromBody] ExamPostDto dto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var teacher = await _context.Users
            .Include(u => u.Subject)
            .FirstOrDefaultAsync(u => u.Id == id);

            if (teacher is null) return NotFound("Teacher not found");

            var group = await _context.Groups.FirstOrDefaultAsync(x => x.Id == dto.GroupId);

            dto.SubjectId = teacher.SubjectId;

            if (group is null) return NotFound("Group not found");

            // Calculate the minimum date and time allowed for the exam (1 day later)
            var minDateTime = DateTime.UtcNow.AddDays(1);

            if (dto.DateTime < minDateTime)
            {
                return BadRequest("Exam date and time must be at least 1 day later.");
            }

            var existingExams = await _context.Exams
            .Where(e => e.GroupId == dto.GroupId)
            .ToListAsync();

            foreach (var existingExam in existingExams)
            {
                // Calculate the time difference between the new exam and each existing exam
                var timeDifference = dto.DateTime.Subtract(existingExam.DateTime).TotalHours;

                // Ensure that the time difference is at least 3 hours
                if (Math.Abs(timeDifference) < 3)
                {
                    return BadRequest("Exams cannot be scheduled less than 3 hours apart for the same group.");
                }
            }

            var exam = new Exam();

            _mapper.Map(dto, exam);

            await _context.Exams.AddAsync(exam);
            await _context.SaveChangesAsync();

            return Ok(exam.Id);
        }

        // PUT api/<ExamController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/<ExamController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var exam = await _context.Exams.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (exam is null) return NotFound();

            _context.Exams.Remove(exam);

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
