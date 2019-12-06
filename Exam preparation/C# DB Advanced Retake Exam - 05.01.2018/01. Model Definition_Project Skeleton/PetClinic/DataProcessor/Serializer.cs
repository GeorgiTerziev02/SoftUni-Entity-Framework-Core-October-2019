namespace PetClinic.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Newtonsoft.Json;
    using PetClinic.Data;
    using PetClinic.DataProcessor.ExportDTOs;

    public class Serializer
    {
        public static string ExportAnimalsByOwnerPhoneNumber(PetClinicContext context, string phoneNumber)
        {
            var animalsByOwnerToExport = context
                .Passports
                .Where(p => p.OwnerPhoneNumber == phoneNumber)
                .Select(p => new
                {
                    p.OwnerName,
                    AnimalName = p.Animal.Name,
                    Age = p.Animal.Age,
                    SerialNumber = p.SerialNumber,
                    RegisteredOn = p.RegistrationDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture)
                })
                .OrderBy(a => a.Age)
                .ThenBy(a => a.SerialNumber)
                .ToList();

            var result = JsonConvert.SerializeObject(animalsByOwnerToExport, Formatting.Indented);

            return result;
        }

        public static string ExportAllProcedures(PetClinicContext context)
        {
            var serializer = new XmlSerializer(typeof(List<ExportProcedureDTO>), new XmlRootAttribute("Procedures"));

            var proceduresToExport = context
                .Procedures
                .OrderBy(p => p.Animal.Passport.RegistrationDate)
                .ThenBy(p => p.Animal.SerialNumber)
                .Select(p => new ExportProcedureDTO
                {
                    Passport = p.Animal.SerialNumber,
                    OwnerNumber = p.Animal.Passport.OwnerPhoneNumber,
                    DateTime = p.Animal.Passport.RegistrationDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture),
                    AnimalAids = p.ProcedureAnimalAids.Select(paa => new ExportAnimalAidDTO
                    {
                        Name = paa.AnimalAid.Name,
                        Price = paa.AnimalAid.Price
                    })
                    .ToList(),
                    TotalPrice = p.ProcedureAnimalAids.Sum(paa=>paa.AnimalAid.Price)
                })
                .ToList();

            var sb = new StringBuilder();
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            using (var writer = new StringWriter(sb))
            {
                serializer.Serialize(writer, proceduresToExport, namespaces);
            }

            return sb.ToString().TrimEnd();
        }
    }
}
