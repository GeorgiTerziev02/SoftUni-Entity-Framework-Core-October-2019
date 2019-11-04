namespace SoftUni
{
    using Microsoft.EntityFrameworkCore;
    using SoftUni.Data;
    using SoftUni.Models;

    using System;
    using System.Linq;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
            SoftUniContext context = new SoftUniContext();

            string result = RemoveTown(context);

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
                .Include(x => x.Manager)
                .Include(x => x.EmployeesProjects)
                .ThenInclude(x => x.Project)
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

                sb.AppendLine($"{empName} - Manager: {managerName}");

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
                        endDate = project.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt");
                    }

                    sb.AppendLine($"--{project.Name} - {project.StartDate.ToString("M/d/yyyy h:mm:ss tt")} - {endDate}");
                }
            }

            return sb.ToString().TrimEnd();
        }

        //Problem 08
        public static string GetAddressesByTown(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var addresses = context
                .Addresses
                .Select(x => new
                {
                    x.AddressText,
                    EmployeesCount = x.Employees.Count,
                    TownName = x.Town.Name
                })
                .OrderByDescending(x => x.EmployeesCount)
                .ThenBy(x => x.TownName)
                .ThenBy(x => x.AddressText)
                .Take(10);

            foreach (var address in addresses)
            {
                sb.AppendLine($"{address.AddressText}, {address.TownName} - {address.EmployeesCount} employees");
            }

            return sb.ToString().TrimEnd();
        }

        //Problem 09
        public static string GetEmployee147(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employee = context
                .Employees
                .Where(e => e.EmployeeId == 147)
                .Select(x => new
                {
                    FullName = x.FirstName + " " + x.LastName,
                    x.JobTitle,
                    Projects = x.EmployeesProjects.Select(y => y.Project.Name).ToList()
                })
                .First();

            sb.AppendLine($"{employee.FullName} - {employee.JobTitle}");

            foreach (var projectName in employee.Projects.OrderBy(x => x))
            {
                sb.AppendLine(projectName);
            }


            return sb.ToString().TrimEnd();
        }

        //Problem 10
        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var departments = context
                .Departments
                .Where(x => x.Employees.Count > 5)
                .Select(x => new
                {
                    x.Name,
                    x.Manager,
                    x.Employees
                })
                .ToList();

            foreach (var dep in departments.OrderBy(x => x.Employees.Count).ThenBy(x => x.Name))
            {
                var managerName = dep.Manager.FirstName + " " + dep.Manager.LastName;
                sb.AppendLine($"{dep.Name} - {managerName}");

                foreach (var employee in dep.Employees.OrderBy(x => x.FirstName).ThenBy(x => x.LastName))
                {
                    var employeeName = employee.FirstName + " " + employee.LastName;
                    sb.AppendLine($"{employeeName} - {employee.JobTitle}");
                }
            }

            return sb.ToString().TrimEnd();
        }

        //Problem 11
        public static string GetLatestProjects(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var projects = context
                .Projects
                .Select(x => new
                {
                    x.Name,
                    x.Description,
                    x.StartDate
                })
                .OrderByDescending(p => p.StartDate)
                .Take(10);

            foreach (var project in projects.OrderBy(p => p.Name))
            {
                sb.AppendLine(project.Name)
                  .AppendLine(project.Description)
                  .AppendLine(project.StartDate.ToString("M/d/yyyy h:mm:ss tt"));
            }

            return sb.ToString().TrimEnd();
        }

        //Problem 12
        public static string IncreaseSalaries(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employees = context
                .Employees
                .Where(e => e.Department.Name == "Engineering" ||
                            e.Department.Name == "Tool Design" ||
                            e.Department.Name == "Marketing" ||
                            e.Department.Name == "Information Services");

            foreach (var emp in employees)
            {
                emp.Salary = emp.Salary * 1.12m;
            }

            context.SaveChanges();

            var empPrint = employees
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName,
                    x.Salary
                });

            foreach (var emp in empPrint.OrderBy(x => x.FirstName).ThenBy(x => x.LastName))
            {
                var fullName = emp.FirstName + " " + emp.LastName;

                sb.AppendLine($"{fullName} (${emp.Salary:f2})");
            }

            return sb.ToString().TrimEnd();
        }

        //Problem 13
        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employees = context
                .Employees
                .Where(x => x.FirstName.StartsWith("Sa"))
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName,
                    x.JobTitle,
                    x.Salary
                });

            foreach (var emp in employees.OrderBy(x => x.FirstName).ThenBy(x => x.LastName))
            {
                sb.AppendLine($"{emp.FirstName + " " + emp.LastName} - {emp.JobTitle} - (${emp.Salary:f2})");
            }

            return sb.ToString().TrimEnd();
        }

        //Problem 14
        public static string DeleteProjectById(SoftUniContext context)
        {
            var project = context
                .Projects
                .Find(2);

            var ep = context.EmployeesProjects.Where(x => x.ProjectId == 2).ToList();

            context.EmployeesProjects.RemoveRange(ep);

            context.Projects.Remove(project);

            context.SaveChangesAsync();

            var projects = context
                .Projects
                .Select(x => x.Name)
                .Take(10);

            return string.Join(Environment.NewLine, projects);
        }

        //Problem 15
        public static string RemoveTown(SoftUniContext context)
        {
            var seattle = context
                .Towns
                .First(t => t.Name == "Seattle");

            var addressesInTown = context
                .Addresses
                .Where(a => a.Town.Name == "Seattle");

            var employeesToRemoveAddress = context
                .Employees
                .Where(e => addressesInTown.Contains(e.Address));

            foreach (var emp in employeesToRemoveAddress)
            {
                emp.AddressId = null;
            }

            context.Addresses.RemoveRange(addressesInTown.ToList());

            int addressesCount = addressesInTown.Count();

            context.Towns.Remove(seattle);
            context.SaveChanges();

            return $"{addressesCount} addresses in Seattle were deleted";
        }
    }
}
