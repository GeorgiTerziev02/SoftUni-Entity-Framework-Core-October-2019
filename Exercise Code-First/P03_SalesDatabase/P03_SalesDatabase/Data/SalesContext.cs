namespace P03_SalesDatabase.Data
{
    using Microsoft.EntityFrameworkCore;
    using P03_SalesDatabase.Data.Models;

    public class SalesContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(DataSettings.connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Product>()
                .Property("Name")
                .IsRequired(true)
                .HasMaxLength(DataValidations.Product.ProductNameMaxLength)
                .IsUnicode(true);

            modelBuilder
                .Entity<Product>()
                .Property("Price")
                .IsRequired(true);
            
            modelBuilder
                .Entity<Product>()
                .Property("Quantity")
                .IsRequired(true);

            modelBuilder
                .Entity<Store>()
                .Property("Name")
                .IsRequired(true)
                .HasMaxLength(DataValidations.Store.StoreNameMaxLength)
                .IsUnicode(true);
        }
    }
}
