using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperSoft.WebAPI.DataAccess;
using SuperSoft.WebAPI.Entities;

namespace SuperSoft.WebAPI.Controllers.Students
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDBContext dbContext;

        public StudentController(ApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IEnumerable<Student>> Get()
            => await dbContext.Students
                     .ToListAsync();

        // GET api/Student/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> Get(int id)
        {
            if (id == 0) return NotFound();

            var student = await dbContext.Students.FindAsync(id);

            if (student is null) return NotFound();

            return Ok(student);
        }

        // POST api/Student
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StudentRequest request)
        {
            dbContext.Add(new Student
            {
                name = request.name,
                dateOfBirth = request.dateOfBirth,
            });
            await dbContext.SaveChangesAsync();

            return StatusCode(201);
        }

        // PUT api/Student/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] StudentRequest request)
        {
            if (id == 0) return NotFound();

            var student = await dbContext.Students.FindAsync(id);

            if (student is null) return NotFound();

            student.name = request.name;
            student.dateOfBirth = request.dateOfBirth;

            return Ok();
        }
    }
}