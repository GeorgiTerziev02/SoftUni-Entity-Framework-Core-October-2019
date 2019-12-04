namespace SoftJail.DataProcessor
{
    using AutoMapper;
    using Data;
    using Newtonsoft.Json;
    using SoftJail.Data.Models;
    using SoftJail.Data.Models.Enums;
    using SoftJail.DataProcessor.ImportDto;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    public class Deserializer
    {
        private static string ErrorMessage = "Invalid Data";

        public static string ImportDepartmentsCells(SoftJailDbContext context, string jsonString)
        {
            var departmentDtos = JsonConvert.DeserializeObject<List<ImportDepartmentDTO>>(jsonString);

            var validDeps = new List<Department>();

            StringBuilder sb = new StringBuilder();

            foreach (var departmentDto in departmentDtos)
            {
                var areCellsInvalid = departmentDto.Cells.Any(c => IsValid(c) == false);
                if (IsValid(departmentDto) == false || areCellsInvalid == true)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var department = Mapper.Map<Department>(departmentDto);

                validDeps.Add(department);

                sb.AppendLine($"Imported {department.Name} with {department.Cells.Count} cells");
            }

            context.Departments.AddRange(validDeps);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportPrisonersMails(SoftJailDbContext context, string jsonString)
        {
            var prisonerDtos = JsonConvert.DeserializeObject<List<ImportPrisonerDTO>>(jsonString);

            StringBuilder sb = new StringBuilder();

            var validPrisoners = new List<Prisoner>();

            foreach (var prisonerDto in prisonerDtos)
            {
                var areAllMailsValid = prisonerDto.Mails.All(IsValid);

                if (IsValid(prisonerDto) == false || areAllMailsValid == false)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var prisoner = Mapper.Map<Prisoner>(prisonerDto);
                validPrisoners.Add(prisoner);

                sb.AppendLine($"Imported {prisoner.FullName} {prisoner.Age} years old");
            }

            context.AddRange(validPrisoners);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportOfficersPrisoners(SoftJailDbContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(List<ImportOfficerDTO>), new XmlRootAttribute("Officers"));

            StringBuilder sb = new StringBuilder();

            List<ImportOfficerDTO> officerDTOs;

            using (var reader = new StringReader(xmlString))
            {
                officerDTOs = (List<ImportOfficerDTO>)serializer.Deserialize(reader);
            }

            var validOfficers = new List<Officer>();

            foreach (var officerDto in officerDTOs)
            {
                var isPositionValid = Enum.IsDefined(typeof(Position), officerDto.Position);
                var isWeaponValid = Enum.IsDefined(typeof(Weapon), officerDto.Weapon);

                if (IsValid(officerDto) == false || isPositionValid == false || isWeaponValid == false)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var officer = new Officer()
                {
                    FullName = officerDto.FullName,
                    DepartmentId = officerDto.DepartmentId,
                    Position = Enum.Parse<Position>(officerDto.Position),
                    Weapon = Enum.Parse<Weapon>(officerDto.Weapon),
                    Salary = officerDto.Salary,
                };

                foreach (var prisoner in officerDto.Prisoners)
                {
                    var officerPrisoner = new OfficerPrisoner()
                    {
                        PrisonerId = prisoner.Id,
                        Officer = officer
                    };

                    officer.OfficerPrisoners.Add(officerPrisoner);
                }

                validOfficers.Add(officer);

                sb.AppendLine($"Imported {officer.FullName} ({officer.OfficerPrisoners.Count} prisoners)");
            }

            context.Officers.AddRange(validOfficers);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static bool IsValid(object entity)
        {
            var context = new System.ComponentModel.DataAnnotations.ValidationContext(entity);
            var results = new List<ValidationResult>();

            var result = Validator.TryValidateObject(entity, context, results, true);

            return result;
        }
    }
}