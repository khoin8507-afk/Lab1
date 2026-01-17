using Lab1.Models;
using Microsoft.EntityFrameworkCore;


namespace Lab1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data
            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, Name = "John Doe", Email = "john.doe@example.com" },
                new Student { Id = 2, Name = "Jane Smith", Email = "jane.smith@example.com" }
            );
        }
    }
}

