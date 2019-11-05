namespace EFCodeFirstTest
{
    using System;
    using System.Linq;
    using Data.Models;
    using EFCodeFirstTest.Data;
    using Microsoft.EntityFrameworkCore;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            using var db = new StudentsDbContext();

            db.Database.Migrate();

            var courses = db
                .Courses
                .Select(c => new
                {
                    c.Name,
                    TotalStudents = c
                        .Students
                        .Where(st => st.Course.Homeworks.Average(h => h.Score) > 2)
                        .Count(),
                    Students = c.Students
                        .Select(st => new
                        {
                            FullName = st.Student.FirstName + " " + st.Student.LastName,
                            Score = st.Student.Homeworks.Average(h => h.Score)
                        })
                })
                .ToList();

            Console.WriteLine(courses.Count);
        }
    }
}
