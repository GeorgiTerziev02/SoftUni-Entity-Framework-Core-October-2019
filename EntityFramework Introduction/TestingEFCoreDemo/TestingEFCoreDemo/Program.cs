using System;
using System.Linq;
using TestingEFCoreDemo.Data;
using TestingEFCoreDemo.Results;
using Microsoft.EntityFrameworkCore;

namespace TestingEFCoreDemo
{
    public class Program
    {
        private static bool Filter(string town)
        {
            return town.Length == 4;
        }

        public static void Main()
        {
            using (var db =new SoftUniContext())
            {
                var towns = db.Towns
                    .Include(t => t.Addresses)
                    .ToList();

                foreach (var town in towns)
                {
                    Console.WriteLine(town.Name);

                    foreach (var address in town.Addresses)
                    {
                        Console.WriteLine($"------{address.AddressText}");
                    }
                }

                var result = db.Towns
                    .Where(t => t.Name.StartsWith("A"))
                    .Select(t=> new TownResultModel
                    {
                        Name = t.Name,
                        Address = t.Addresses.Select(a => a.AddressText)
                    })
                    .ToList()
                    .Where(t => Filter(t.Name));

                var employeeResult = db.Employees
                    .Select(e => new EmployeeResultModel
                    {
                        Name = e.FirstName + " " + e.LastName,
                        Department = e.Department.Name
                    })
                    .ToList();

                foreach (var emp in employeeResult)
                {
                    Console.WriteLine($"{emp.Department}: {emp.Name}");
                }
            }
        }
    }
}
