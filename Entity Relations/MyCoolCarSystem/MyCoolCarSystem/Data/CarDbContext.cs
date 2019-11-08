namespace MyCoolCarSystem.Data
{
    using Microsoft.EntityFrameworkCore;
    using MyCoolCarSystem.Data.Configurations;
    using MyCoolCarSystem.Data.Models;
    using System.Linq;
    using System.Reflection;

    public class CarDbContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }

        public DbSet<Make> Makes { get; set; }

        public DbSet<Model> Models { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<CarPurchase> Purchases { get; set; }

        public DbSet<Address> Addresses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(DataSettings.DefaultConnection);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new MakeConfiguration());
            //modelBuilder.ApplyConfiguration(new CarConfiguration());
            //modelBuilder.ApplyConfiguration(new CarPurchaseConfiguration());
            //modelBuilder.ApplyConfiguration(new CustomerConfiguration());

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
