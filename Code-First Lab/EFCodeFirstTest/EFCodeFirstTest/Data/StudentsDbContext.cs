namespace EFCodeFirstTest.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class StudentsDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(DataSettings.DefaultConnection);
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
