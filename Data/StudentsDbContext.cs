using DependencyInjection_WebAPI.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
// this is the comment used for testing
// test commit 2
namespace DependencyInjection_WebAPI.Data
{
    public class StudentsDbContext : DbContext
    {
        public StudentsDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<College> Colleges { get; set; }
    }
}
