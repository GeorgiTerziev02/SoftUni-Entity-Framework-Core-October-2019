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
          
            db.SaveChanges();
        }
    }
}
