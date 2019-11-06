namespace P03_SalesDatabase.Data
{
    using Microsoft.EntityFrameworkCore;
    using P03_SalesDatabase.Data.Models;

    public class SalesContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Store> Stores { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Sale> Sales { get; set; }

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
                .HasKey(p => p.ProductId);

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
                .HasKey(s => s.StoreId);

            modelBuilder
                .Entity<Store>()
                .Property("Name")
                .IsRequired(true)
                .HasMaxLength(DataValidations.Store.StoreNameMaxLength)
                .IsUnicode(true);

            modelBuilder
                .Entity<Customer>()
                .HasKey(c => c.CustomerId);

            modelBuilder
                .Entity<Customer>()
                .Property("Name")
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(DataValidations.Customer.CustomerNameMaxLength);

            modelBuilder
                .Entity<Customer>()
                .Property("Email")
                .IsUnicode(false)
                .IsRequired(true)
                .HasMaxLength(DataValidations.Customer.EmailMaxLength);

            modelBuilder
                .Entity<Customer>()
                .Property("CreditCardNumber")
                .IsRequired(true);

            modelBuilder
                .Entity<Sale>()
                .HasKey(s => s.SaleId);

            modelBuilder
                .Entity<Sale>()
                .Property("CustomerId")
                .IsRequired(true);
            
            modelBuilder
                .Entity<Sale>()
                .Property("StoreId")
                .IsRequired(true);
            
            modelBuilder
                .Entity<Sale>()
                .Property("ProductId")
                .IsRequired(true); 
            
            modelBuilder
                .Entity<Sale>()
                .Property("Date")
                .IsRequired(true);

            modelBuilder
                .Entity<Sale>()
                .HasOne(s => s.Product)
                .WithMany(p => p.Sales)
                .HasForeignKey(s => s.ProductId);

            modelBuilder
                .Entity<Sale>()
                .HasOne(s => s.Customer)
                .WithMany(c => c.Sales)
                .HasForeignKey(s => s.SaleId);

            modelBuilder
                .Entity<Sale>()
                .HasOne(s => s.Store)
                .WithMany(store => store.Sales)
                .HasForeignKey(s => s.StoreId);
        }
    }
}
