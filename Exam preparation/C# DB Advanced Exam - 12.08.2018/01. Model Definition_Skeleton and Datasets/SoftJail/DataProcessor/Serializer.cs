namespace SoftJail.DataProcessor
{

    using Data;
    using Newtonsoft.Json;
    using SoftJail.DataProcessor.ExportDto;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    public class Serializer
    {
        public static string ExportPrisonersByCells(SoftJailDbContext context, int[] ids)
        {
            var prisoners = context
                .Prisoners
                .Where(p => ids.Contains(p.Id))
                .Select(p => new
                {
                    Id = p.Id,
                    Name = p.FullName,
                    CellNumber = p.Cell.CellNumber,
                    Officers = p.PrisonerOfficers.Select(po => new
                    {
                        OfficerName = po.Officer.FullName,
                        Department = po.Officer.Department.Name
                    })
                    .OrderBy(o => o.OfficerName),
                    TotalOfficerSalary = decimal.Parse(p.PrisonerOfficers.Sum(po => po.Officer.Salary).ToString("f2"))
                })
                .OrderBy(p => p.Name)
                .ThenBy(p => p.Id)
                .ToList();

            var output = JsonConvert.SerializeObject(prisoners, Formatting.Indented);

            return output;
        }

        public static string ExportPrisonersInbox(SoftJailDbContext context, string prisonersNames)
        {
            var prisonerNames = prisonersNames.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList();

            var prisoners = context
                .Prisoners
                .Where(p => prisonerNames.Contains(p.FullName))
                .Select(p => new ExportPrisonerDTO
                {
                    Id = p.Id,
                    Name = p.FullName,
                    IncarcerationDate = p.IncarcerationDate.ToString("yyyy-MM-dd"),
                    EncryptedMessages = p.Mails.Select(m => new ExportMessageDTO
                    {
                        Description = ReverseString(m.Description)
                    })
                    .ToList()
                })
                .OrderBy(p => p.Name)
                .ThenBy(p => p.Id)
                .ToList();

            var serializer = new XmlSerializer(typeof(List<ExportPrisonerDTO>), new XmlRootAttribute("Prisoners"));
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            StringBuilder sb = new StringBuilder();

            using (var writer = new StringWriter(sb))
            {
                serializer.Serialize(writer, prisoners, namespaces);
            }

            return sb.ToString().TrimEnd();
        }

        private static string ReverseString(string stringToReverse)
        {
            string reversed = string.Empty;

            for (int i = stringToReverse.Length - 1; i >= 0; i--)
            {
                reversed += stringToReverse[i];
            }

            return reversed;
        }
    }
}