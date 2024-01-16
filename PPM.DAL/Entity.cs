using Microsoft.EntityFrameworkCore;
using PPM.Model;

namespace PPM.DAL
{
    public class PPMContext : DbContext
    {
        // DbSet for each entity in the database
        public DbSet<Project> Project { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<ProjectEmployees> ProjectEmployees { get; set; }

        // Default constructor with connection string
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=RHJ-9F-D219\\SQLEXPRESS; Database=prolifics_project_Maneger; Integrated Security=SSPI;TrustServerCertificate=True;"
            );
        }

        // Constructor with options for dependency injection
        public PPMContext(DbContextOptions<PPMContext> options) : base(options)
        {
        }

        // Model configuration
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure composite primary key for ProjectEmployees
            modelBuilder.Entity<ProjectEmployees>().HasKey(e => new { e.projectId, e.employeeId });

            // Configure primary keys for each entity
            modelBuilder.Entity<Project>().HasKey(e => new { e.ProjectId });
            modelBuilder.Entity<Employee>().HasKey(e => new { e.EmployeeId });
            modelBuilder.Entity<Role>().HasKey(e => new { e.RoleId });
        }
    }
}
