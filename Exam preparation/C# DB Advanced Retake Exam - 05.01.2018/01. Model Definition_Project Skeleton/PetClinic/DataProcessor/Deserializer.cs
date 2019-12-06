namespace PetClinic.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Newtonsoft.Json;
    using PetClinic.Data;
    using PetClinic.DataProcessor.DTOs;
    using PetClinic.Models;

    public class Deserializer
    {
        private const string ErrorMessage = "Error: Invalid data.";

        public static string ImportAnimalAids(PetClinicContext context, string jsonString)
        {
            var dtos = JsonConvert.DeserializeObject<List<ImportAnimalAidDTO>>(jsonString);

            StringBuilder sb = new StringBuilder();

            List<AnimalAid> validAnimalAids = new List<AnimalAid>();

            foreach (var dto in dtos)
            {
                bool isNameUsed = context.AnimalAids.Any(aa => aa.Name == dto.Name);
                if (IsValid(dto) == false || isNameUsed == true)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var animalAid = new AnimalAid()
                {
                    Name = dto.Name,
                    Price = dto.Price
                };

                validAnimalAids.Add(animalAid);
                context.AnimalAids.Add(animalAid);
                context.SaveChanges();

                sb.AppendLine($"Record {animalAid.Name} successfully imported.");
            }

            return sb.ToString().TrimEnd();
        }

        public static string ImportAnimals(PetClinicContext context, string jsonString)
        {
            throw new NotImplementedException();
        }

        public static string ImportVets(PetClinicContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(List<ImportVetDTO>), new XmlRootAttribute("Vets"));

            List<ImportVetDTO> vetDTOs;

            using (var reader = new StringReader(xmlString))
            {
                vetDTOs = (List<ImportVetDTO>)serializer.Deserialize(reader);
            }

            StringBuilder sb = new StringBuilder();

            foreach (var dto in vetDTOs)
            {
                bool isPhoneUsed = context.Vets.Any(v => v.PhoneNumber == dto.PhoneNumber);

                if (IsValid(dto) == false || isPhoneUsed == true)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var vet = new Vet()
                {
                    Age = dto.Age,
                    Name = dto.Name,
                    PhoneNumber = dto.PhoneNumber,
                    Profession = dto.Profession
                };

                context.Vets.Add(vet);
                context.SaveChanges();

                sb.AppendLine($"Record {vet.Name} successfully imported.");
            }

            return sb.ToString().TrimEnd();
        }

        public static string ImportProcedures(PetClinicContext context, string xmlString)
        {
            throw new NotImplementedException();
        }

        public static bool IsValid(object obj)
        {
            var context = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var results = new List<ValidationResult>();

            bool result = Validator.TryValidateObject(obj, context, results, true);

            return result;
        }
    }
}
