using demo2.Models;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace demo2
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var db = new ExcursionDbContext();


            db.Database.EnsureCreated();


        }
    }
}