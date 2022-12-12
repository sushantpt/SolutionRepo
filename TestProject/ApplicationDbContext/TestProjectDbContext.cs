using Domain.Entity.Student;
using Domain.Entity.Teacher;
using Microsoft.EntityFrameworkCore;

namespace TestProject.ApplicationDbContext
{
    public class TestProjectDbContext:DbContext
    {
        public TestProjectDbContext(DbContextOptions<TestProjectDbContext> options) : base(options)
        {

        }

        public DbSet<StudentEntityModel> Student { get; set; }
        public DbSet<TeacherEntityModel> Teacher { get; set; }
    }
}
