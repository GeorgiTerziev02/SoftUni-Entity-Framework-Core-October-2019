namespace TeisterMask.DataProcessor
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Globalization;
    using System.Xml.Serialization;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    using Data;
    using TeisterMask.Data.Models;
    using TeisterMask.Data.Models.Enums;
    using TeisterMask.DataProcessor.ImportDto;

    using Newtonsoft.Json;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedProject
            = "Successfully imported project - {0} with {1} tasks.";

        private const string SuccessfullyImportedEmployee
            = "Successfully imported employee - {0} with {1} tasks.";

        public static string ImportProjects(TeisterMaskContext context, string xmlString)

        {
            var xmlSerializer = new XmlSerializer(typeof(List<ImportProjectDTO>), new XmlRootAttribute("Projects"));

            List<ImportProjectDTO> projectDTOs;

            using (var reader = new StringReader(xmlString))
            {
                projectDTOs = (List<ImportProjectDTO>)xmlSerializer.Deserialize(reader);
            }

            StringBuilder sb = new StringBuilder();
            var validProjects = new List<Project>();

            foreach (var dto in projectDTOs)
            {
                bool areTasksInvalid = dto.Tasks.Any(t => IsValid(t) == false);

                if (IsValid(dto) == false)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var project = new Project()
                {
                    Name = dto.Name,
                    OpenDate = DateTime.ParseExact(dto.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                };

                if (string.IsNullOrWhiteSpace(dto.DueDate))
                {
                    project.DueDate = null;
                }
                else
                {
                    project.DueDate = DateTime.ParseExact(dto.DueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }

                foreach (var taskDto in dto.Tasks)
                {
                    if (IsValid(taskDto) == false)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    var task = new Task()
                    {
                        Name = taskDto.Name,
                        OpenDate = DateTime.ParseExact(taskDto.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        DueDate = DateTime.ParseExact(taskDto.DueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        ExecutionType = (ExecutionType)Enum.ToObject(typeof(ExecutionType), taskDto.ExecutionType),
                        LabelType = (LabelType)Enum.ToObject(typeof(LabelType), taskDto.LabelType)
                    };

                    if (task.OpenDate < project.OpenDate)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (task.DueDate > project.DueDate)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    project.Tasks.Add(task);
                }

                validProjects.Add(project);
                sb.AppendLine(string.Format(SuccessfullyImportedProject, project.Name, project.Tasks.Count));
            }

            context.AddRange(validProjects);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportEmployees(TeisterMaskContext context, string jsonString)
        {
            var employeeDtos = JsonConvert.DeserializeObject<List<ImportEmployeeDTO>>(jsonString);

            StringBuilder sb = new StringBuilder();

            var validEmployees = new List<Employee>();

            foreach (var dto in employeeDtos)
            {
                if (IsValid(dto) == false)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var employee = new Employee()
                {
                    Email = dto.Email,
                    Phone = dto.Phone,
                    Username = dto.Username
                };

                foreach (var taskId in dto.Tasks.Distinct().ToList())
                {
                    bool taskExist = context.Tasks.Any(t => t.Id == taskId);

                    if (taskExist == false)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    var employeeTask = new EmployeeTask()
                    {
                        Employee = employee,
                        TaskId = taskId
                    };

                    employee.EmployeesTasks.Add(employeeTask);
                }
                validEmployees.Add(employee);
                sb.AppendLine(string.Format(SuccessfullyImportedEmployee, employee.Username, employee.EmployeesTasks.Count));
            }

            context.Employees.AddRange(validEmployees);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}