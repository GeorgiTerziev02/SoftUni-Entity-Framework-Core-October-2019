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
            var animalDtos = JsonConvert.DeserializeObject<List<ImportAnimalDTO>>(jsonString);

            StringBuilder sb = new StringBuilder();

            foreach (var animalDto in animalDtos)
            {
                bool isNumberUser = context.Passports.Any(p => p.SerialNumber == animalDto.Passport.SerialNumber);

                if (IsValid(animalDto) == false || isNumberUser == true || IsValid(animalDto.Passport) == false)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var animal = new Animal()
                {
                    Age = animalDto.Age,
                    Name = animalDto.Name,
                    Type = animalDto.Type
                };

                var passport = new Passport()
                {
                    SerialNumber = animalDto.Passport.SerialNumber,
                    OwnerName = animalDto.Passport.OwnerName,
                    OwnerPhoneNumber = animalDto.Passport.OwnerPhoneNumber,
                    RegistrationDate = DateTime.Parse(animalDto.Passport.RegistrationDate),
                    Animal = animal
                };

                animal.Passport = passport;

                context.Animals.Add(animal);
                context.SaveChanges();

                sb.AppendLine($"Record {animal.Name} Passport №: {passport.SerialNumber} successfully imported.");
            }

            return sb.ToString().TrimEnd();
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
            var serializer = new XmlSerializer(typeof(List<ImportProcedureDTO>), new XmlRootAttribute("Procedures"));

            List<ImportProcedureDTO> procedureDTOs;

            using (var reader = new StringReader(xmlString))
            {
                procedureDTOs = (List<ImportProcedureDTO>)serializer.Deserialize(reader);
            }

            StringBuilder sb = new StringBuilder();

            foreach (var dto in procedureDTOs)
            {
                bool vetExist = context.Vets.Any(v => v.Name == dto.VetName);
                bool animalExist = context.Animals.Any(a => a.SerialNumber == dto.AnimalSerialNumber);
                bool aidExist = dto.AnimalAids.All(aa => context.AnimalAids.Any(x => aa.Name == x.Name));

                var aidsCount = dto.AnimalAids.Select(x=>x.Name).Count();
                var aidsCountAfterDistict = dto.AnimalAids.Select(x => x.Name).Distinct().Count();

                if (IsValid(dto) == false || vetExist == false
                    || animalExist == false || aidExist == false || aidsCount != aidsCountAfterDistict)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var procedure = new Procedure()
                {
                    Animal = context.Animals.FirstOrDefault(a => a.SerialNumber == dto.AnimalSerialNumber),
                    DateTime = DateTime.Parse(dto.DateTime),
                    Vet = context.Vets.First(v => v.Name == dto.VetName)
                };

                foreach (var animalAid in dto.AnimalAids)
                {
                    var animalAidProcedure = new ProcedureAnimalAid()
                    {
                        Procedure = procedure,
                        AnimalAid = context.AnimalAids.First(aa => aa.Name == animalAid.Name)
                    };

                    procedure.ProcedureAnimalAids.Add(animalAidProcedure);
                }

                context.Procedures.Add(procedure);
                context.SaveChanges();

                sb.AppendLine($"Record successfully imported.");
            }

            return sb.ToString().TrimEnd();
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
