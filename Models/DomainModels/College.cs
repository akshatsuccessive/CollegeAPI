namespace DependencyInjection_WebAPI.Models.DomainModels
{
    public class College
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; } 
        public string? ImageURL { get; set; }     // Now this property can have null values (means optional field)
    }
}
