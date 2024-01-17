namespace DependencyInjection_WebAPI.Models.DomainModels
{
    public class Student
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string? ImageURL { get; set; }

        // Relationship between models
        public Guid CourseId { get; set; }
        public Guid CollegeId { get; set; }

        // Navigation properties
        public Course Course { get; set; }
        public College College { get; set; }
    }
}
