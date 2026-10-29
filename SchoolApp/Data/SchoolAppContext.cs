
using Microsoft.EntityFrameworkCore;
using SchoolApp.Models;

namespace SchoolApp.Data
{
    public class SchoolAppContext : DbContext
    {
        public SchoolAppContext(DbContextOptions<SchoolAppContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Student { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Keep Name nullable in the database so old test records can load.
            modelBuilder.Entity<Student>()
                .Property(s => s.Name)
                .IsRequired(false);

            modelBuilder.Entity<Student>()
                .Property(s => s.Phone)
                .IsRequired(false);
        }
    }
}
