using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjection_WebAPI.Controllers
{
    // https://localhost:7051/api/Students
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        // GET: https://localhost:7051/api/Students
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            return Ok();
        }
    }
}
