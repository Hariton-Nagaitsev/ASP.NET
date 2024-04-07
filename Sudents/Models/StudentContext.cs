using Microsoft.EntityFrameworkCore;

namespace Sudents.Models
{
    public class StudentContext : DbContext
    {
        public StudentContext(DbContextOptions<StudentContext> options) : base(options) { }
        public DbSet<Student> Studs { get; set; }
    }
}
