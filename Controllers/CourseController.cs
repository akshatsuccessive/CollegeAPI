using DependencyInjection_WebAPI.Data;
using DependencyInjection_WebAPI.Models.DTO;
using DependencyInjection_WebAPI.Models.DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjection_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly StudentsDbContext context;
        public CourseController(StudentsDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetAllCourses() 
        {
            var courses = context.Courses.ToList();

            var coursesDTO = new List<CourseDTO>();
            foreach(var course in courses) 
            {
                coursesDTO.Add(new CourseDTO()
                {
                    Id = course.Id,
                    Name = course.Name,
                });
            }

            return Ok(coursesDTO);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetCourseById([FromRoute] Guid id)
        {
            var course = context.Courses.Find(id);
            if (course == null)
            {
                return NotFound();
            }
            var courseDTO = new CourseDTO()
            {
                Id = course.Id,
                Name = course.Name,
            };
            return Ok(courseDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddCourse([FromBody] AddCourseRequestDTO request)
        {
            var CourseDomainModel = new Course()
            {
                Name = request.Name
            };

            await context.Courses.AddAsync(CourseDomainModel);
            await context.SaveChangesAsync();

            var courseDTO = new CourseDTO()
            {
                Id = CourseDomainModel.Id,
                Name = CourseDomainModel.Name
            };

            return CreatedAtAction(nameof(GetCourseById), new { id = courseDTO.Id } ,courseDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse([FromRoute] Guid id, [FromBody] UpdateCourseRequestDTO request)
        {
            var course = await context.Courses.FindAsync(id);
            if (course == null) 
            { 
                return NotFound();
            }
            course.Name = request.Name;

            var courseDTO = new CourseDTO() 
            { 
                Id = course.Id,
                Name = course.Name 
            };

            await context.SaveChangesAsync();
            return Ok(courseDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse([FromRoute] Guid id)
        {
            var course = await context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            context.Courses.Remove(course);
            await context.SaveChangesAsync();
            return Ok("Course Delete Sucessfully");
        }
    }
}
