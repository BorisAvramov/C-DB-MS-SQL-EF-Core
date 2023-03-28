using Advanced_Querying_LAB.Data;
using Advanced_Querying_LAB.Data2;
using Advanced_Querying_LAB.Data2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.ComponentModel.DataAnnotations;
using Z.EntityFramework.Plus;

namespace Advanced_Querying_LAB
{
    public class Program
    {
        static void Main(string[] args)
        {
            MusicHubContext musicContext= new MusicHubContext();
            SoftUniContext softUniContext = new SoftUniContext();


            // !!!!! ----------------  SQL COMMANDS IN EF  ---------------------

            //// universal SQL command for update, create, insert, delete
            //musicContext.Database.ExecuteSqlRaw("UPDATE Songs SET Name = 'What Goes Around 2' WHERE Id=1");

            //// get / select songs from db with native sql command
            //// without .ToArray() result is IQueryable / with . Array() is IEnumerable
            //// works only when => required columns must always be selected

            ////ATTENTION BE CAREFULL FOR SQL INJECTION FROM THE USER'S INPUT

            //int ID = int.Parse(Console.ReadLine());

            //var songs = musicContext.Songs.FromSqlInterpolated($"SELECT * FROM Songs WHERE Id < {ID}").ToArray();

            //foreach (var song in songs)
            //{
            //    Console.WriteLine(song.Name);
            //}


            //// execut stored precedures SQL db

            //int employeeID = int.Parse(Console.ReadLine());

            //var town = softUniContext.Database.ExecuteSqlInterpolated($"EXEC [dbo].[sp_IncreaseSalaryBy1] {employeeID}").ToString();

            //Console.WriteLine(town);

            //Console.WriteLine("success");


            // !!!!!!!!!! ---------------------------    OBJECT STATE TRACKING ATTACHED DETACHED


            // song is Attached / Tracked => changes are available
            //var song = musicContext.Songs.FirstOrDefault(s => s.Id == 1);

            //// DETACH SONG FROM CONTEXT => CHANGES ARE NOT AVAILABLE
            //musicContext.Entry(song).State = EntityState.Detached;

            //song.Price += 1;


            //// ATTACH SONG TO CONTEXT
            //musicContext.Entry(song).State = EntityState.Modified;

            //musicContext.SaveChanges();


            // !!!!!!!!!    --------- BULK OPERATIONS ------

            //DOWNLOAD NUGET PACKAGE Z.EntityFramework.Plus.EFCore
            // DELETE AND UPDATE MULTIPLE ROWS IN DB WITH ONE EF QUERY
            // return rows affected

            //softUniContext.Employees.Where(e => e.EmployeeId > 291).Delete();

            // UPDATE MULTIPLE ROWS IN DB WITH ONE EF QUERY

            //softUniContext.Employees
            //    .Where(e => e.EmployeeId > 289)
            //    .Update(e => new Employee { Salary = 50000 });


            // !!!!!!!! ================== LOADINGS -> Excplicit, Eager, Lazy

            // Explicit Loading - rare use

            var employee1 = softUniContext.Employees.First();

            // here address is null, because NAVIGATIONAL PROP ADDRESS IS NOT LOADED -> HERE PROP IS TRACKING BUT NOT LOADED
            // NullReferenceException
            //Console.WriteLine(employee.Address.AddressText);

            var employee2 = softUniContext.Employees
                .Select (e => new
                {
                    Address = e.Address
                })
                .First();
            
            //here address is loaded because projection (select) but object is not tracking / not attached
            Console.WriteLine(employee2.Address.AddressText);

            // LOADING TARGETS = OBJECT TO BE ATTACHED / TRACKED AND SAME TIME TO INCLUDE NAVIGATIONAL PROPERTIES = IT IS POSSIBLE TO CHANGE NAVIGATIONAL PROPERTIES


            //========================================================================================================================================
            // 1 way => EXPLICIT LOADING  <with .Load method and .Entry(object)>

            softUniContext.Entry(employee1).Reference(e => e.Department).Load();

            Console.WriteLine(employee1.Department.Name);

            employee1.Department.Name += "!!!!";

            Console.WriteLine(employee1.Department.Name);

            //softUniContext.SaveChanges();


            // get virtual prop manager name of employee's department

            var departmentOfEmployee1 = employee1.Department;
            softUniContext.Entry(departmentOfEmployee1).Reference(d => d.Manager).Load();

            Console.WriteLine(departmentOfEmployee1.Manager.FirstName);



            // load collection
            var town = softUniContext.Towns.First();

            softUniContext.Entry(town).Collection(t => t.Addresses).Load();

            Console.WriteLine(town.Addresses.Count());
            Console.WriteLine(String.Join(", ", town.Addresses.Select(a => a.AddressText)));


            //==========================================================================================================================================
            // 2 way EAGER LOADING = MUCH BETTER THAN EXPLICIT => .Include


            var employee3 = softUniContext.Employees
                     .Include(e => e.Address)
                     .ThenInclude(a => a.Town)
                     .First();

            Console.WriteLine(employee3.Address.AddressText);
            Console.WriteLine(employee3.Address.Town.Name);



            //==========================================================================================================================================
            // 3 way LAZY LOADING =
            // 1 STEP - VIRTUAL MAKES IT POSSIBLE
            // 2 STEP - Install NuGet Package - Microsoft.EntityFrameworkCore.Proxies
            // 3 STEP - write that code optionsBuilder.UseLazyLoadingProxies() in OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            //  result is the same as Explicit Loading, but it is like magic, info is loaded under


           var employee4 = softUniContext.Employees.First();

            Console.WriteLine(employee4.Department.Name);
            Console.WriteLine(employee4.Department.Manager.LastName);

            








        }
    }
}