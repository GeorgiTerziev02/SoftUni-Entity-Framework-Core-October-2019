namespace MyCoolCarSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Data.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> customer)
        {
            customer
                .HasOne(c => c.Address)
                .WithOne(a => a.Customer)
                .HasForeignKey<Address>(a => a.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
