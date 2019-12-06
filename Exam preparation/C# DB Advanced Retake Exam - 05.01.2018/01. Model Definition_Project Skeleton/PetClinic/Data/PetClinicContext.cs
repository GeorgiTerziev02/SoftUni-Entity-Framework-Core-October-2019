namespace PetClinic.Data
{
    using Microsoft.EntityFrameworkCore;
    using PetClinic.Models;

    public class PetClinicContext : DbContext
    {
        public PetClinicContext() { }

        public PetClinicContext(DbContextOptions options)
            :base(options) { }

        public DbSet<Animal> Animals { get; set; }
        public DbSet<AnimalAid> AnimalAids { get; set; }
        public DbSet<Passport> Passports { get; set; }
        public DbSet<Procedure> Procedures { get; set; }
        public DbSet<ProcedureAnimalAid> ProcedureAnimalAids { get; set; }
        public DbSet<Vet> Vets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ProcedureAnimalAid>(paa =>
            {
                paa.HasKey(p => new { p.ProcedureId, p.AnimalAidId });

                paa.HasOne(p => p.AnimalAid)
                    .WithMany(a => a.AnimalAidProcedures)
                    .HasForeignKey(p => p.AnimalAidId);

                paa.HasOne(pa => pa.Procedure)
                    .WithMany(p => p.ProcedureAnimalAids)
                    .HasForeignKey(pa => pa.ProcedureId);
            });

            builder.Entity<AnimalAid>().HasIndex(a => a.Name).IsUnique();

            builder.Entity<Vet>().HasIndex(v => v.PhoneNumber).IsUnique();

            builder.Entity<Procedure>(procedure =>
            {
                procedure.HasOne(p => p.Vet)
                    .WithMany(v => v.Procedures)
                    .HasForeignKey(p => p.VetId);

                procedure.HasOne(p => p.Animal)
                    .WithMany(a => a.Procedures)
                    .HasForeignKey(p => p.AnimalId);
            });

            builder.Entity<Animal>()
                .HasOne(a => a.Passport)
                .WithOne(p => p.Animal)
                .HasForeignKey<Animal>(a => a.SerialNumber);  
        }
    }
}
