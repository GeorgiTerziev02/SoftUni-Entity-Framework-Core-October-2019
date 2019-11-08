namespace MyCoolCarSystem
{
    using Microsoft.EntityFrameworkCore;
    using MyCoolCarSystem.Data;
    using MyCoolCarSystem.Data.Models;
    using System;
    using System.Linq;

    public class Program
    {
        public static void Main(string[] args)
        {
            using var db = new CarDbContext();

            db.Database.Migrate();

            var result = db.Purchases
                .Select(p => new
                {
                    p.Price,
                    p.PurchaseDate,
                    Customer = new
                    {
                        Name = p.Customer.FirstName + " " + p.Customer.LastName,
                        p.Customer.Address.Town
                    },

                    Car = new
                    {
                        Make = p.Car.Model.Make.Name,
                        Model = p.Car.Model.Name,
                        p.Car.Vin
                    }
                })
                .ToList(); 
            
            db.SaveChanges();
        }
    }
}
