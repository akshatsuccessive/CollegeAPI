namespace DependencyInjection_WebAPI.Models.DTO
{
    public class CollegeDTO
    {
        // DTO has the properties that we want to expose back to the client
        // DTO is subset of DomainModel
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string? ImageURL { get; set; }
    }
}
