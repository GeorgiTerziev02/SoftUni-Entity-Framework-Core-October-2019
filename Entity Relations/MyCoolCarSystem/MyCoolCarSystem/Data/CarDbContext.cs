﻿namespace MyCoolCarSystem.Data
{
    using Microsoft.EntityFrameworkCore;
    using MyCoolCarSystem.Data.Models;

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
            modelBuilder.Entity<Car>(car =>
            {
                car
                    .HasIndex(c => c.Vin)
                    .IsUnique();

                car
                    .HasOne(c => c.Model)
                    .WithMany(m => m.Cars)
                    .HasForeignKey(c => c.ModelId)
                    .OnDelete(DeleteBehavior.Restrict);
            });


            modelBuilder.Entity<Make>(make =>
            {
                make
                    .HasIndex(m => m.Name)
                    .IsUnique(); 

                make
                    .HasMany(m => m.Models)
                    .WithOne(model => model.Make)
                    .HasForeignKey(m => m.MakeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });


            modelBuilder.Entity<CarPurchase>(purchase=>
            {
                purchase.HasKey(p => new { p.CustomerId, p.CarId });

                purchase
                    .HasOne(p => p.Customer)
                    .WithMany(c => c.Purchases)
                    .HasForeignKey(p => p.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);

                purchase
                    .HasOne(p => p.Car)
                    .WithMany(c => c.Owners)
                    .HasForeignKey(p => p.CarId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Customer>(customer =>
            {
                customer
                    .HasOne(c => c.Address)
                    .WithOne(a => a.Customer)
                    .HasForeignKey<Address>(a=>a.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
