using DependencyInjection_WebAPI.Data;
using DependencyInjection_WebAPI.Models.DTO;
using DependencyInjection_WebAPI.Models.DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjection_WebAPI.Controllers
{
    // https://localhost:7051/api/Students
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly StudentsDbContext context;
        public StudentsController(StudentsDbContext context)
        {
            this.context = context;
        }

        // GET: https://localhost:7051/api/Students
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            var students = context.Students.ToList();

            // response : model to DTO
            var studentsDTO = new List<StudentDTO>();
            foreach(var student in students)
            {
                studentsDTO.Add(new StudentDTO()
                {
                    Id = student.Id,
                    Name = student.Name,
                    Age = student.Age,
                    ImageURL = student.ImageURL,
                    CourseId = student.CourseId,
                    CollegeId = student.CollegeId,
                });
            }

            return Ok(studentsDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById([FromRoute] Guid id)
        {
            var student = await context.Students.FindAsync(id);
            if(student == null)
            {
                return NotFound();
            }
            var studentDTO = new StudentDTO()
            {
                Id = student.Id,
                Name = student.Name,
                Age = student.Age,
                ImageURL = student.ImageURL,
                CourseId = student.CourseId,
                CollegeId = student.CollegeId
            };
            return Ok(studentDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] AddStudentRequestDTO request)
        {
            // DTO to DomainModel
            var StudentDomainModel = new Student()
            {
                Name = request.Name,
                Age = request.Age,
                ImageURL = request.ImageURL,
                CourseId = request.CourseId,
                CollegeId = request.CollegeId
            };

            await context.Students.AddAsync(StudentDomainModel);
            await context.SaveChangesAsync();

            // DomainModel to DTO and return back to client
            var StudentDTO = new StudentDTO()
            {
                Id = StudentDomainModel.Id,
                Name = StudentDomainModel.Name,
                Age = StudentDomainModel.Age,
                ImageURL = StudentDomainModel.ImageURL,
                CollegeId = StudentDomainModel.CollegeId,
                CourseId = StudentDomainModel.CourseId
            };

            return CreatedAtAction(nameof(GetStudentById), new {id = StudentDTO.Id}, StudentDTO);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent([FromRoute] Guid id, [FromBody] UpdateStudentRequestDTO request)
        {
            var student = await context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            student.Name = request.Name;
            student.Age = request.Age;
            student.ImageURL = request.ImageURL;

            var StudentDTO = new StudentDTO()
            {
                Id = student.Id,
                Name = student.Name,
                Age = student.Age,
                ImageURL = student.ImageURL,
                CollegeId= student.CollegeId,
                CourseId = student.CourseId
            };

            await context.SaveChangesAsync();
            return Ok(StudentDTO);
        }


    }
}
