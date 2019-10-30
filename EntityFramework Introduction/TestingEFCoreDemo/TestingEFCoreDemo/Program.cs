using System;
using System.Linq;
using TestingEFCoreDemo.Data;

namespace TestingEFCoreDemo
{
    public class Program
    {
        public static void Main()
        {
            using (var db =new SoftUniContext())
            {
                var towns = db.Towns.ToList();

                Console.WriteLine(towns.Count);
            }
        }
    }
}
