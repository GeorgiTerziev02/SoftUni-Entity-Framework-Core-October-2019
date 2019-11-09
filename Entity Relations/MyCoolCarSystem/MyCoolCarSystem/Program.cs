namespace MyCoolCarSystem
{
    using Microsoft.EntityFrameworkCore;
    using MyCoolCarSystem.Data;
    using MyCoolCarSystem.Data.Models;
    using MyCoolCarSystem.Results;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class Program
    {
        public static void Main(string[] args)
        {
            using var db = new CarDbContext();

            db.Database.Migrate();

            var result = db.Purchases
                .Select(p => new PurchaseResultModel
                {
                    Price = p.Price,
                    PurchaseDate = p.PurchaseDate,
                    Customer = new CustomerResultModel
                    {
                        Name = p.Customer.FirstName + " " + p.Customer.LastName,
                        Town = p.Customer.Address.Town
                    },

                    Car = new CarResultModel
                    {
                        Make = p.Car.Model.Make.Name,
                        Model = p.Car.Model.Name,
                        Vin = p.Car.Vin
                    }
                })
                .ToList();

            var make = db.Makes.FirstOrDefault(m=>m.Name=="Mercedes");

            var model = new Model
            {
                Modification = "500",
                Name = "CL",
                Year = 3000
            };

            var validationContext = new ValidationContext(model);
            var validationResults = new List<ValidationResult>();

            Validator.TryValidateObject(model, validationContext, validationResults, true);
            Validator.ValidateObject(model, validationContext, true);

            make.Models.Add(model);

            db.SaveChanges();
        }
    }
}
