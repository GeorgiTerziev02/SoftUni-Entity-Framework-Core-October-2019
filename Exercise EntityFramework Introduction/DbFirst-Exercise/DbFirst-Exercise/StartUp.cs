namespace SoftUni
{
    using SoftUni.Data;
    using SoftUni.Models;
    using System;
    using System.Linq;
    using System.Text;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            SoftUniContext context = new SoftUniContext();

            string result = GetEmployeesInPeriod(context);

            Console.WriteLine(result);
        }

        //Problem 03

        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employees = context
                .Employees
                .OrderBy(e => e.EmployeeId)
                .Select(e => new
                {
                    Name = e.FirstName + " " + e.LastName + " " + e.MiddleName,
                    e.JobTitle,
                    e.Salary
                })
                .ToList();

            foreach (var emp in employees)
            {
                sb.AppendLine($"{emp.Name} {emp.JobTitle} {emp.Salary:f2}");
            }

            return sb.ToString().TrimEnd();
        }

        // Problem 04

        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employees = context
                .Employees
                .Select(e => new
                {
                    e.FirstName,
                    e.Salary
                })
                .Where(e => e.Salary > 50000)
                .OrderBy(e => e.FirstName)
                .ToList();

            foreach (var emp in employees)
            {
                sb.AppendLine($"{emp.FirstName} - {emp.Salary:f2}");
            }

            return sb.ToString().TrimEnd();
        }

        //Problem 05

        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employees = context
                .Employees
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    DepartmentName = e.Department.Name,
                    e.Salary
                })
                .Where(e => e.DepartmentName == "Research and Development")
                .OrderBy(e => e.Salary)
                .ThenByDescending(e => e.FirstName)
                .ToList();

            foreach (var emp in employees)
            {
                sb.AppendLine($"{emp.FirstName} {emp.LastName} from {emp.DepartmentName} - ${emp.Salary:f2}");
            }

            return sb.ToString().TrimEnd();
        }

        //Problem 06
        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            Address address = new Address
            {
                AddressText = "Vitoshka 15",
                TownId = 4
            };

            Employee nakov = context
                .Employees
                .First(e => e.LastName == "Nakov");

            nakov.Address = address;

            context.SaveChanges();

            var addresses = context
                .Employees
                .OrderByDescending(e => e.AddressId)
                .Select(e => e.Address.AddressText)
                .Take(10)
                .ToList();

            foreach (var a in addresses)
            {
                sb.AppendLine(a);
            }

            return sb.ToString().TrimEnd();
        }

        //Problem 07
        public static string GetEmployeesInPeriod(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employees = context
                .Employees
                .Where(e => e.EmployeesProjects.Any(p => p.Project.StartDate.Year >= 2001 && p.Project.StartDate.Year <= 2003))
                .Take(10);

            foreach (var emp in employees)
            {
                var empName = emp.FirstName + " " + emp.LastName;
                string managerName = string.Empty;

                if (emp.Manager is null)
                {
                    managerName = null;
                }
                else
                {
                    managerName = emp.Manager.FirstName + " " + emp.Manager.LastName;
                }

                sb.AppendLine($"{empName} – Manager: {managerName}");

                var projects = emp
                    .EmployeesProjects
                    .Select(ep => new
                    {
                        ep.Project.Name,
                        ep.Project.StartDate,
                        ep.Project.EndDate
                    })
                    .ToList();

                foreach (var project in projects)
                {
                    string endDate = string.Empty;
                    if (project.EndDate is null)
                    {
                        endDate = "not finished";
                    }
                    else
                    {
                        endDate = String.Format("M/d/yyyy h:mm:ss tt", project.EndDate);
                    }

                    sb.AppendLine($"--{project.Name} - {String.Format("M/d/yyyy h:mm:ss tt", project.StartDate)} - {endDate}");
                }
            }

            return sb.ToString().TrimEnd();
        }
    }
}
