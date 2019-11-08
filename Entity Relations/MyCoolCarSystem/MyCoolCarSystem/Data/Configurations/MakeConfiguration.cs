namespace MyCoolCarSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Data.Models;

    public class MakeConfiguration : IEntityTypeConfiguration<Make>
    {
        public void Configure(EntityTypeBuilder<Make> make)
        {
            make
                .HasIndex(m => m.Name)
                .IsUnique();

            make
                .HasMany(m => m.Models)
                .WithOne(model => model.Make)
                .HasForeignKey(m => m.MakeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
