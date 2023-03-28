using Microsoft.EntityFrameworkCore;
using SoftUni.Data;
using SoftUni.Models;
using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace SoftUni
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            SoftUniContext suc = new SoftUniContext();

            //03. Employees Full Information
            Console.WriteLine(GetEmployeesFullInformation(suc));


            // 04. Employees with Salary Over 50 000
            //Console.WriteLine( GetEmployeesWithSalaryOver50000(suc));

            // 05. Employees from Research and Development
            //Console.WriteLine( GetEmployeesFromResearchAndDevelopment(suc) );


            // 06. Adding a New Address and Updating Employee
            // Console.WriteLine(AddNewAddressToEmployee(suc));


            // 07. Employees and Projects
            //Console.WriteLine(GetEmployeesInPeriod(suc));

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

        //public static string GetEmployeesWithSalaryOver50000 (SoftUniContext context)
        //{
        //    StringBuilder sb = new StringBuilder();

        //    var employeesWithSalaryOver50000 = context.Employees
        //        .Where(e => e.Salary > 50000)
        //        .OrderBy(e => e.FirstName)
        //        .Select(e => new
        //        {
        //            e.FirstName,
        //            e.Salary

        //        }).ToArray();

        //    foreach (var emp in employeesWithSalaryOver50000)
        //    {
        //        sb.AppendLine($"{emp.FirstName} - {emp.Salary:F2}");
        //    }

        //    return sb.ToString().TrimEnd();


        //}


        //public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        //{

        //    StringBuilder sb = new StringBuilder();

        //    var employees = context
        //        .Employees
        //        .OrderBy(e => e.Salary).ThenByDescending(e => e.FirstName)
        //        .Where(e => e.Department.Name == "Research and Development")
        //        //.Include(e => e.Department.Name)
        //        .Select(e => new
        //        {
        //            e.FirstName,
        //            e.LastName,
        //            e.Department.Name,
        //            e.Salary
        //        }).ToArray();

        //    foreach (var empl in employees)
        //    {
        //        sb.AppendLine($"{empl.FirstName} {empl.LastName} from {empl.Name} - ${empl.Salary:f2}");
        //    }

        //    return sb.ToString().TrimEnd();

        //}


        //public static string AddNewAddressToEmployee(SoftUniContext context)
        //{
        //    StringBuilder sb = new StringBuilder();


        //    context.Addresses.Add
        //        (
        //            new Address
        //            {
        //                AddressText = "Vitoshka 15",
        //                TownId = 4
        //            }

        //        );

        //    context.SaveChanges();

        //    var employee = context.Employees.FirstOrDefault(e => e.LastName == "Nakov");
        //    var address = context.Addresses.FirstOrDefault(a => a.AddressText == "Vitoshka 15" && a.TownId == 4);
        //    if (employee != null)
        //    {
        //        employee.AddressId = address.AddressId;
        //        context.SaveChanges();
        //    }

        //    var addresses = context
        //        .Addresses
        //        .OrderByDescending(a => a.AddressId)
        //        .Select(a => new { AddressText = a.AddressText }).Take(10);


        //    foreach (var add in addresses)
        //    {
        //        sb.AppendLine(add.AddressText);
        //    }

        //    return sb.ToString().TrimEnd();
        //}


        //public static string GetEmployeesInPeriod(SoftUniContext context)
        //{
        //    StringBuilder sb = new StringBuilder();

        //    var employees1 = context
        //            .Employees
        //            .Where(e => e.EmployeesProjects.Any(p => p.Project.StartDate.Year >= 2001 && p.Project.StartDate.Year <= 2003))
        //            .Select(e => new
        //            {
        //                EmployeeFirstName = e.FirstName,
        //                EmployeeLastName = e.LastName,
        //                ManagerFirstName = e.Manager.FirstName,
        //                ManagerLastName = e.Manager.LastName,
        //                EmployeeProjects = e.EmployeesProjects
        //                                    .Select( ep => new
        //                                    {
        //                                        ep.Project.Name,
        //                                        StartDate = ep.Project.StartDate.ToString("M/d/yyyy h:mm:ss tt"),
        //                                        EndDate = ep.Project.EndDate.HasValue ? ep.Project.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt") : "not finished" 
        //                                    }).ToArray(),


        //            }).ToArray();



        //    foreach (var empl in employees1)
        //    {
        //        sb.AppendLine($"{empl.EmployeeFirstName} {empl.EmployeeLastName} - Manager: {empl.ManagerFirstName} {empl.ManagerLastName}");
        //        foreach (var proj in empl.EmployeeProjects)
        //        {
        //            sb.AppendLine($"--{proj.Name} - {proj.StartDate} - {proj.EndDate}");
        //        }

        //    }


        //    return sb.ToString().TrimEnd();

        //}

    }
}
