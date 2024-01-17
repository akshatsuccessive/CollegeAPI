using DependencyInjection_WebAPI.Data;
using DependencyInjection_WebAPI.Models.DTO;
using DependencyInjection_WebAPI.Models.DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjection_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollegeController : ControllerBase
    {
        private readonly StudentsDbContext context;
        // constructor injecting
        public CollegeController(StudentsDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAllColleges()
        {
            // 1. Get Data from the database -> this is Domain Models
            var colleges = context.Colleges.ToList();

            // 2. Map this Domain Models to DTO
            var collegesDTO = new List<CollegeDTO>();
            foreach (var college in colleges)
            {
                collegesDTO.Add(new CollegeDTO()
                {
                    Id = college.Id,
                    Name = college.Name,
                    City = college.City,
                    ImageURL = college.ImageURL
                });
            }

            // 3. Return the DTO back to client
            return Ok(collegesDTO);
        }

        // get a single collge by Id
        [HttpGet("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetCollegeById([FromRoute] Guid id)
        {
            var college = context.Colleges.Find(id);    // Find property only needs a primary key as a parameter
            if (college == null)
            {
                return NotFound();
            }
            var collegeDTO = new CollegeDTO
            {
                Id = college.Id,
                Name = college.Name,
                City = college.City,
                ImageURL = college.ImageURL
            };

            return Ok(collegeDTO);
        }


        // We need a DTO for POST
        [HttpPost]
        public async Task<IActionResult> AddCollege([FromBody] AddCollegeRequestDTO request)
        {
            // 1. Map or Convert DTO to Domain Model
            var CollegeDomainModel = new College
            {
                Name = request.Name,
                City = request.City,
                ImageURL = request.ImageURL
            };

            // 2. Use Domain Model to create College
            await context.Colleges.AddAsync(CollegeDomainModel);
            await context.SaveChangesAsync();


            // 3. Map domain model back to DTO
            var collegeDTO = new CollegeDTO
            {
                Id = CollegeDomainModel.Id,
                Name = CollegeDomainModel.Name,
                City = CollegeDomainModel.City,
                ImageURL = CollegeDomainModel.ImageURL
            };


            // In post request we return at 201 
            return CreatedAtAction(nameof(GetCollegeById), new { id = collegeDTO.Id }, collegeDTO);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCollege([FromRoute] Guid id, [FromBody] UpdateCollegeRequestDTO request) 
        {
            var college = await context.Colleges.FindAsync(id);
            if(college == null)
            {
                return NotFound();
            }

            college.Name = request.Name;
            college.City = request.City;
            college.ImageURL = request.ImageURL;


            // Domain Model to DTO
            var collegeDTO = new CollegeDTO
            {
                Id = college.Id,
                Name = college.Name,
                City = college.City,
                ImageURL = college.ImageURL
            };

            await context.SaveChangesAsync();
            return Ok(collegeDTO);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCollege([FromRoute] Guid id)
        {
            var college = await context.Colleges.FindAsync(id);
            if(college == null)
            {
                return NotFound();
            }
            context.Colleges.Remove(college);
            await context.SaveChangesAsync();
            return Ok("College Deleted Successfully");
        }
    }
}
