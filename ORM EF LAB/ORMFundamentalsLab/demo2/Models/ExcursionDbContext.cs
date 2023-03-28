using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo2.Models
{
    public class ExcursionDbContext : DbContext
    {


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-6ADIQGK\\SQLEXPRESS;Integrated Security=true;Database=Excursion;TrustServerCertificate=true");
        }

        public DbSet<Tourist> Tourists { get; set; }
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}
