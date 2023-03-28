using SoftUni.Data;
using System.Text;

namespace SoftUni
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            SoftUniContext suc = new SoftUniContext();

            Console.WriteLine( GetEmployeesFullInformation(suc));

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
    }
}