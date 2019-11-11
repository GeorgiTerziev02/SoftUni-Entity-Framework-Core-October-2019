namespace MyCoolCarSystem
{
    using Microsoft.EntityFrameworkCore;
    using MyCoolCarSystem.Data;
    using MyCoolCarSystem.Data.Models;
    using MyCoolCarSystem.Results;
    using Data.Queries;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Z.EntityFramework.Plus;

    public class Program
    {
        public static void Main(string[] args)
        {
            using var db = new CarDbContext();

            db.Database.Migrate();

            object update = db
                .Cars
                .Where(c=>c.Price<20000)
                .Update(c => new Car { Price = c.Price * 1.2m});
        }
    }
}
