using DatabaseFirst_Demo.Models;
using System.Net.WebSockets;

namespace DatabaseFirst_Demo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // connection string to docker container: "Server=localhost; User Id=sa; Password=avramov!87; Database=SoftUni; TrustServerCertificate = true"
            // connection string to local sqlServer: "Server=.; Integrated Security=true; Database=SoftUni; TrustServerCertificate = true"


            // !!! Install NuGet Packages
            // Microsoft.EntityFrameworkCore.SqlServer
            // Microsoft.EntityFrameworkCore.Design

            // !!! type this in powershell (command-line) to connect to server and database for creating scaffold of tables in VS (in c# classes, properties) and create dbcontects and dbSet for each table from database
            // dotnet ef dbcontext scaffold "Server=DESKTOP-6ADIQGK\SQLEXPRESS;Integrated Security=true;Database=NationalTouristSitesOfBulgaria; TrustServerCertificate = true" Microsoft.EntityFrameworkCore.SqlServer



           // ====================================================================================================================

            // initialize dbcontext class to access dbsets (tables is sql, class / objects in C#)

            var db = new SoftUniContext();

            //Console.WriteLine("Write town to be added to db");
            //string townToAdd = Console.ReadLine();


            ////ADD / CREATE ENTITY / ROW IN DB

            //db.Towns.Add(new Town { Name = townToAdd });
            //db.SaveChanges();

            //Console.WriteLine("Write town to be searcher");
            //string town = Console.ReadLine();


            //SELECT / READ ENTITY / ROW FROM DB

            //var emplLivedInCertainTown2 = db.Employees.Where(e => e.Address.Town.Name == town).Select(e => new { name = e.FirstName, town = e.Address.Town.Name, salary = e.Salary }).OrderByDescending(e => e.salary).ToList();


            //foreach (var curEmpl in emplLivedInCertainTown2)
            //{
            //    Console.WriteLine($"{curEmpl.name} => {curEmpl.town}, {curEmpl.salary}");
            //}



            //var departmentsAndCountOfEmployeesIn = db.Employees.GroupBy(e => e.Department.Name).Select(e => new {name = e.Key, count = e.Count() }).ToList();

            //Console.WriteLine("PAUSE!");

            //foreach (var department in departmentsAndCountOfEmployeesIn)
            //{
            //    Console.WriteLine($"{department.name} => {department.count}");
            //}




            //var emplLivedInCertainTown = db.Employees
            //                    .Join(db.Addresses,
            //                          emp => emp.AddressId,
            //                          add => add.AddressId,
            //                          (emp, add) => new { Salary = emp.Salary, FirstName = emp.FirstName, TownId = add.TownId }
            //                     )
            //                    .Join(db.Towns,
            //                           a => a.TownId,
            //                           t => t.TownId,
            //                           (a, t) => new { Salary = a.Salary, FirstName = a.FirstName ,TownName = t.Name })
            //                    .Where(emp => emp.TownName == town && emp.Salary > 30000 )
            //                    .Select(e => new {firstName = e.FirstName, townName = e.TownName, salary = e.Salary}).ToList()
            //                    .OrderBy(e => e.firstName);




            // UPDATE ENTITY / ROW FROM DB
            //var employees = db.Employees.ToList();


            //foreach (var empl in employees)
            //{

            //    empl.Salary *= 1.1M;

            //}

            //db.SaveChanges();




          
        }
    }
}