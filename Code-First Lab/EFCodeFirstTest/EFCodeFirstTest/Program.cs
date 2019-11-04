namespace EFCodeFirstTest
{
    using System;
    using System.Linq;
    using Data;

    public class Program
    {
        public static void Main(string[] args)
        {
            using var db = new StudentsDbContext();

            db.Database.EnsureCreated();

            db.Students.ToList();
        }
    }
}
