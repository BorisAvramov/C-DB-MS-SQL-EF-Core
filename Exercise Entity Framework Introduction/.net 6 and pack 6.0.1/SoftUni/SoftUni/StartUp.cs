using Microsoft.EntityFrameworkCore;
using SoftUni.Data;
using SoftUni.Models;
using System.Collections.Immutable;
using System.Reflection;
using System.Text;

namespace SoftUni
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            // POWER SHELL
            // dotnet ef dbcontext scaffold "Server=DESKTOP-6ADIQGK\SQLEXPRESS;Database=SoftUni;Integrated Security=true" Microsoft.EntityFrameworkCore.SqlServer -o Data/Models

            SoftUniContext suc = new SoftUniContext();

            //03. Employees Full Information
            //Console.WriteLine(GetEmployeesFullInformation(suc));

            // 04. Employees with Salary Over 50 000
            //Console.WriteLine(GetEmployeesWithSalaryOver50000(suc));

            // 05. Employees from Research and Development
            //Console.WriteLine(GetEmployeesFromResearchAndDevelopment(suc));


            // 06. Adding a New Address and Updating Employee
            //Console.WriteLine(AddNewAddressToEmployee(suc));

            //07. Employees and Projects

            //Console.WriteLine(GetEmployeesInPeriod(suc));

            //08. Addresses by Town
            //Console.WriteLine(GetAddressesByTown(suc));

            //09. Employee 147
            //Console.WriteLine(GetEmployee147(suc));

            //10. Departments with More Than 5 Employees

            //Console.WriteLine( GetDepartmentsWithMoreThan5Employees(suc));

            //11. Find Latest 10 Projects

            //Console.WriteLine(GetLatestProjects(suc));

            //12. Increase Salaries

            //Console.WriteLine(IncreaseSalaries(suc));

            //13. Find Employees by First Name Starting With Sa

            //Console.WriteLine(GetEmployeesByFirstNameStartingWithSa(suc));

            //14. Delete Project by Id

            Console.WriteLine(DeleteProjectById(suc));


        }



        public static string DeleteProjectById(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();


            var project = context.Projects.Find(2);

            var emplToDel = context.Employees.Where(e => e.Projects.Any(p => p.ProjectId == 2));

            context.Employees.RemoveRange(emplToDel);


            //context.Employees.ToList().ForEach(e => e.Projects.Remove(project));

            context.Projects.Remove(project);

            context.SaveChanges();


            //Project project = context
            //        .Projects.FirstOrDefault(p => p.ProjectId == 2);

            //Employee employee = context.Employees.FirstOrDefault(e => e.Projects.Contains(project));


            //context.Employees.ToList().ForEach(e => e.Projects.Remove(project));

            //context.Projects.ToList().ForEach(p => p.Employees.Remove(employee));

            //context.SaveChanges();


            //context.Projects.Remove(project);

            //context.SaveChanges();


            var projects = context.Projects.ToArray();




            foreach (var proj in projects)
            {
                sb.AppendLine(proj.Name);
            }


            return sb.ToString().TrimEnd();
        }

        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
        {

            StringBuilder sb = new StringBuilder();

            var employees = context
                    .Employees
                    .Where(e => e.FirstName.ToLower().StartsWith("sa"))
                    .OrderBy(e => e.FirstName)
                    .ThenBy(e => e.LastName)
                    .ToArray();

            foreach (var empl in employees)
            {
                sb.AppendLine($"{empl.FirstName} {empl.LastName} - {empl.JobTitle} - (${empl.Salary:f2})");

            }
            return sb.ToString().TrimEnd();

        }


        public static string  IncreaseSalaries(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            List <string> targetDepartments = new List<string>() { "Engineering", "Tool Design", "Marketing", "Information Services" };

            var employees = context
                    .Employees
                    .Where(e => targetDepartments.Contains(e.Department.Name))
                    .OrderBy(e => e.FirstName)
                    .ThenBy(e => e.LastName)
                    .ToArray();
            foreach (var emp in employees)
            {
                emp.Salary *= 1.12m;
            }

            context.SaveChanges();


            foreach (var emp in employees)
            {
                sb.AppendLine($"{emp.FirstName} {emp.LastName} (${emp.Salary:f2})");

            }

            return sb.ToString().TrimEnd();

        }

        public static string GetLatestProjects(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var projects = context
                    .Projects
                    .Select(p => new
                    {
                        p.Name,
                        p.Description,
                        p.StartDate
                    })
                    .OrderByDescending(p => p.StartDate)
                    .Take(10)
                    .OrderBy(p => p.Name)
                    .ToArray();
            
            foreach (var proj in projects)
            {
                sb.AppendLine(proj.Name);
                sb.AppendLine(proj.Description);
                sb.AppendLine(proj.StartDate.ToString("M/d/yyyy h:mm:ss tt"));
            }
            return sb.ToString().TrimEnd();

        }


        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var departments = context
                    .Departments
                    .Select(d => new
                    {
                        DepartmentName = d.Name,
                        ManagerFirstName = d.Manager.FirstName,
                        ManagerLastName = d.Manager.LastName,

                        Employees = d.Employees

                    })
                    .Where(d => d.Employees.Count > 5)
                    .OrderBy(d => d.Employees.Count)
                    .ThenBy(d => d.DepartmentName)
                    .ToArray();

            foreach (var dep in departments)
            {
                sb.AppendLine($"{dep.DepartmentName} - {dep.ManagerFirstName}  {dep.ManagerLastName}");

                foreach (var emp in dep.Employees.OrderBy(e => e.FirstName).ThenBy(e => e.LastName))
                {
                    sb.AppendLine($"{emp.FirstName} {emp.LastName} - {emp.JobTitle}");

                }

            }

            return sb.ToString().TrimEnd();

        }


        public static string GetEmployee147(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();
            var employee = context
                    .Employees
                    .Select(e => new
                    {
                        Id = e.EmployeeId,
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        JobTitle = e.JobTitle,
                        Projects = e.Projects,
                     

                    })
                    .FirstOrDefault(e => e.Id == 147);

            if(employee != null)
            {

                sb.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");

                foreach (var proj in employee.Projects.OrderBy(p => p.Name))
                {
                    sb.AppendLine(proj.Name);

                }
            }


            return sb.ToString().TrimEnd();
        }



        public static string GetAddressesByTown(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var addresses = context
                    .Addresses
                    .Select(a => new
                    {
                        AddressText = a.AddressText,
                        TownName = a.Town.Name,
                        EmployeesCount = a.Employees.Count
                    })

                    .OrderByDescending(a => a.EmployeesCount)
                    .ThenBy(a => a.TownName)
                    .ThenBy(a => a.AddressText)
                     .Take(10)
                    .ToArray();

            foreach (var address in addresses)
            {
                sb.AppendLine($"{address.AddressText}, {address.TownName} - {address.EmployeesCount} employees");
            }

            return sb.ToString().TrimEnd();
        }



        public static string GetEmployeesInPeriod(SoftUniContext suc)
        {

            StringBuilder sb = new StringBuilder();


            var employees = suc
                    .Employees
                    .Select(e => new
                    {
                        EemplployeeProjecst = e.Projects,
                        EmployeeFirstName = e.FirstName,
                        EmployeeLastName = e.LastName,
                        ManagerFirstName = e.Manager.FirstName,
                        ManagerLastName = e.Manager.LastName
                    })
                    .Where(e => e.EemplployeeProjecst.Any(p => p.StartDate.Year >= 2001 && p.StartDate.Year <= 2003))
                    .Take(10)
                    .ToArray();

            foreach (var empl in employees)
            {
                sb.AppendLine($"{empl.EmployeeFirstName} {empl.EmployeeLastName} - Manager: {empl.ManagerFirstName} {empl.ManagerLastName}");
                foreach (var proj in empl.EemplployeeProjecst)
                {
                    var endDate = proj.EndDate.HasValue ? proj.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt") : "not finished";

                    sb.AppendLine($"--{proj.Name} - {proj.StartDate.ToString("M/d/yyyy h:mm:ss tt")} -  {endDate}");

                }

            }

            return sb.ToString().TrimEnd();

        }

        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var resultAllEmployees = context
                .Employees
                .OrderBy(e => e.EmployeeId)
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.MiddleName,
                    e.JobTitle,
                    e.Salary
                })
                .ToArray();

            foreach (var empl in resultAllEmployees)
            {
                sb.AppendLine($"{empl.FirstName} {empl.LastName} {empl.MiddleName} {empl.JobTitle} {empl.Salary:F2}");
            }

            return sb.ToString().TrimEnd();


        }

        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employeesWithSalaryOver50000 = context.Employees
                .Where(e => e.Salary > 50000)
                .OrderBy(e => e.FirstName)
                .Select(e => new
                {
                    e.FirstName,
                    e.Salary

                }).ToArray();

            foreach (var emp in employeesWithSalaryOver50000)
            {
                sb.AppendLine($"{emp.FirstName} - {emp.Salary:F2}");
            }

            return sb.ToString().TrimEnd();


        }


        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {

            StringBuilder sb = new StringBuilder();

            var employees = context
                .Employees
                .OrderBy(e => e.Salary).ThenByDescending(e => e.FirstName)
                .Where(e => e.Department.Name == "Research and Development")
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.Department.Name,
                    e.Salary
                }).ToArray();

            foreach (var empl in employees)
            {
                sb.AppendLine($"{empl.FirstName} {empl.LastName} from {empl.Name} - ${empl.Salary:f2}");
            }

            return sb.ToString().TrimEnd();

        }


        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();


            context.Addresses.Add
                (
                    new Address
                    {
                        AddressText = "Vitoshka 15",
                        TownId = 4
                    }

                );

            context.SaveChanges();

            var employee = context.Employees.FirstOrDefault(e => e.LastName == "Nakov");
            var address = context.Addresses.FirstOrDefault(a => a.AddressText == "Vitoshka 15" && a.TownId == 4);
            if (employee != null)
            {
                employee.AddressId = address.AddressId;
                context.SaveChanges();
            }

            var addresses = context
                .Addresses
                .OrderByDescending(a => a.AddressId)
                .Select(a => new { AddressText = a.AddressText }).Take(10);


            foreach (var add in addresses)
            {
                sb.AppendLine(add.AddressText);
            }

            return sb.ToString().TrimEnd();
        }
    }
}