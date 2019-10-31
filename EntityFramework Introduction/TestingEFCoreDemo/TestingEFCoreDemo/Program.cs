using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using TestingEFCoreDemo.Data;
using TestingEFCoreDemo.Results;

namespace TestingEFCoreDemo
{
    public class Program
    {
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
                    .ToList();
            }
        }
    }
}
