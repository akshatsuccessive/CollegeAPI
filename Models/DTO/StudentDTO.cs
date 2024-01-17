using DependencyInjection_WebAPI.Models.DomainModels;

namespace DependencyInjection_WebAPI.Models.DTO
{
    public class StudentDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string? ImageURL { get; set; }

        // Relationship between models
        public Guid CourseId { get; set; }
        public Guid CollegeId { get; set; }
    }
}
