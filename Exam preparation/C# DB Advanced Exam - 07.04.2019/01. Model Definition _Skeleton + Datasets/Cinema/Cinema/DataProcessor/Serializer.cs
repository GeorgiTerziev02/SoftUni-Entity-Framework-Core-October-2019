namespace Cinema.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using AutoMapper.QueryableExtensions;
    using Cinema.DataProcessor.ExportDto;
    using Data;
    using Newtonsoft.Json;

    public class Serializer
    {
        public static string ExportTopMovies(CinemaContext context, int rating)
        {
            var movies = context
                .Movies
                .Where(m => m.Rating >= rating)
                .Where(m => m.Projections.Any(p => p.Tickets.Count > 0))
                //.Where(m => m.Projections.Any(p => p.Tickets.Any(t => t.Customer != null)))
                .OrderByDescending(m => m.Rating)
                .ThenByDescending(m => m.Projections.Sum(p => p.Tickets.Sum(t => t.Price)))
                .Take(10)
                .Select(m => new
                {
                    MovieName = m.Title,
                    Rating = m.Rating.ToString("f2"),
                    TotalIncomes = m.Projections.Sum(p => p.Tickets.Sum(t => t.Price)).ToString("f2"),
                    Customers = m.Projections.SelectMany(p => p.Tickets.Select(t => new ExportCustomerDTO
                    {
                        FirstName = t.Customer.FirstName,
                        LastName = t.Customer.LastName,
                        Balance = t.Customer.Balance.ToString("f2")
                    }))
                    .OrderByDescending(c => c.Balance)
                    .ThenBy(c => c.FirstName)
                    .ThenBy(c => c.LastName)
                    .ToList()
                })
                .ToList();

            var output = JsonConvert.SerializeObject(movies, Formatting.Indented);

            return output;
        }

        public static string ExportTopCustomers(CinemaContext context, int age)
        {
            List<ExportTopCustomerDTO> customersToExport = context
                .Customers
                .Where(c => c.Age >= age)
                .OrderByDescending(c => c.Tickets.Sum(t => t.Price))
                .Select(c => new ExportTopCustomerDTO
                {
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    SpentMoney = c.Tickets.Sum(t => t.Price).ToString("f2"),
                    SpentTime = TimeSpan.FromMilliseconds(c.Tickets
                                            .Sum(t => t.Projection.Movie.Duration.TotalMilliseconds)).ToString(@"hh\:mm\:ss")
                })
                .Take(10)
                .ToList();

            var serializer = new XmlSerializer(typeof(List<ExportTopCustomerDTO>), new XmlRootAttribute("Customers"));

            StringBuilder sb = new StringBuilder();

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            using (var writer = new StringWriter(sb))
            {
                serializer.Serialize(writer, customersToExport, namespaces);
            }

            return sb.ToString().TrimEnd();
        }
    }
}