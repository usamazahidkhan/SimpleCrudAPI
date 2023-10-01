using Microsoft.EntityFrameworkCore;
using SuperSoft.WebAPI.Entities;

namespace SuperSoft.WebAPI.DataAccess
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        { }

        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var student = modelBuilder.Entity<Student>();
            student.Property(c => c.name).IsRequired();
            student.Property(c => c.name).HasMaxLength(50);

            base.OnModelCreating(modelBuilder);
        }
    }
}
