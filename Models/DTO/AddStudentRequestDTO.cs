namespace DependencyInjection_WebAPI.Models.DTO
{
    public class AddStudentRequestDTO
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string? ImageURL { get; set; }
        public Guid CourseId { get; set; }
        public Guid CollegeId { get; set; }
    }
}
