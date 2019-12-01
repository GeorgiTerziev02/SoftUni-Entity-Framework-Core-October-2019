namespace Cinema.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using AutoMapper;
    using Cinema.Data.Models;
    using Cinema.DataProcessor.ImportDto;
    using Data;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";
        private const string SuccessfulImportMovie
            = "Successfully imported {0} with genre {1} and rating {2}!";
        private const string SuccessfulImportHallSeat
            = "Successfully imported {0}({1}) with {2} seats!";
        private const string SuccessfulImportProjection
            = "Successfully imported projection {0} on {1}!";
        private const string SuccessfulImportCustomerTicket
            = "Successfully imported customer {0} {1} with bought tickets: {2}!";

        public static string ImportMovies(CinemaContext context, string jsonString)
        {
            var movieDtos = JsonConvert.DeserializeObject<List<ImportMovieDTO>>(jsonString);

            StringBuilder sb = new StringBuilder();

            var validMovies = new List<Movie>();

            foreach (var movieDto in movieDtos)
            {
                if (IsValid(movieDto) == false)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var movie = Mapper.Map<Movie>(movieDto);
                validMovies.Add(movie);
                sb.AppendLine(string.Format(SuccessfulImportMovie, movie.Title, movie.Genre.ToString(), movie.Rating.ToString("f2")));
            }

            context.Movies.AddRange(validMovies);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportHallSeats(CinemaContext context, string jsonString)
        {
            var hallDtos = JsonConvert.DeserializeObject<List<ImportHallDTO>>(jsonString);

            StringBuilder sb = new StringBuilder();

            var validHalls = new List<Hall>();
            var seats = new List<Seat>();

            foreach (var hallDto in hallDtos)
            {
                if (IsValid(hallDto) == false)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var hall = new Hall
                {
                    Name = hallDto.Name,
                    Is3D = hallDto.Is3D,
                    Is4Dx = hallDto.Is4Dx
                };

                for (int i = 0; i < hallDto.Seats; i++)
                {
                    var seat = new Seat()
                    {
                        Hall = hall
                    };

                    hall.Seats.Add(seat);
                    seats.Add(seat);
                }
                string projectionType;

                if (hall.Is3D == false && hall.Is4Dx == false)
                {
                    projectionType = "Normal";
                }
                else if (hall.Is3D == true && hall.Is4Dx == true)
                {
                    projectionType = "4Dx/3D";
                }
                else if (hall.Is3D == true && hall.Is4Dx == false)
                {
                    projectionType = "3D";
                }
                else
                {
                    projectionType = "4Dx";
                }

                sb.AppendLine(string.Format(SuccessfulImportHallSeat, hall.Name, projectionType, hall.Seats.Count));
            }

            context.Halls.AddRange(validHalls);
            context.Seats.AddRange(seats);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportProjections(CinemaContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(List<ImportProjectionDTO>), new XmlRootAttribute("Projections"));

            List<ImportProjectionDTO> projectionDTOs;

            using (var reader = new StringReader(xmlString))
            {
                projectionDTOs = (List<ImportProjectionDTO>)serializer.Deserialize(reader);
            }

            var validProjections = new List<Projection>();
            StringBuilder sb = new StringBuilder();

            foreach (var projectionDto in projectionDTOs)
            {
                bool isHallValid = context.Halls.Any(h => h.Id == projectionDto.HallId);
                bool isMovieValid = context.Movies.Any(m => m.Id == projectionDto.MovieId);

                if (IsValid(projectionDto) == false || isHallValid == false || isMovieValid == false)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var projection = Mapper.Map<Projection>(projectionDto);
                validProjections.Add(projection);

                var movie = context.Movies.Find(projection.MovieId);

                sb.AppendLine(string.Format(SuccessfulImportProjection, movie.Title,
                                    projection.DateTime.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)));
            }

            context.AddRange(validProjections);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportCustomerTickets(CinemaContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(List<ImportCustomerDTO>), new XmlRootAttribute("Customers"));

            List<ImportCustomerDTO> customerDTOs;

            using (var reader = new StringReader(xmlString))
            {
                customerDTOs = (List<ImportCustomerDTO>)serializer.Deserialize(reader);
            }

            StringBuilder sb = new StringBuilder();
            var validCustomers = new List<Customer>();
            var validTickets = new List<Ticket>();

            foreach (var customerDto in customerDTOs)
            {
                bool areTicketsValid = customerDto.Tickets.All(t=>IsValid(t));

                if (IsValid(customerDto) == false || areTicketsValid == false)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var customer = new Customer
                {
                    FirstName = customerDto.FirstName,
                    LastName = customerDto.LastName,
                    Age = customerDto.Age,
                    Balance = customerDto.Balance,
                };

                foreach (var ticketDto in customerDto.Tickets)
                {
                    var ticket = new Ticket()
                    {
                        Customer = customer,
                        Price = ticketDto.Price,
                        ProjectionId = ticketDto.ProjectionId
                    };

                    customer.Tickets.Add(ticket);
                    validTickets.Add(ticket);
                }

                validCustomers.Add(customer);

                sb.AppendLine(string.Format(SuccessfulImportCustomerTicket, customer.FirstName, customer.LastName, customer.Tickets.Count));
            }

            context.AddRange(validCustomers);
            context.AddRange(validTickets);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static bool IsValid(object entity)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(entity);
            var validationResults = new List<ValidationResult>();

            var result = Validator.TryValidateObject(entity, validationContext, validationResults, true);

            return result;
        }
    }
}