using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst_Demo.Models
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-6ADIQGK\\SQLEXPRESS;Integrated Security=true;Database=CodeFirstDemo;TrustServerCertificate=true");
            //optionsBuilder.UseSqlServer("Server=.;User Id=sa;Password=avramov!87;Database=CodeFirstDemo;TrustServerCertificate=true");

        }



        public DbSet<Category> Categories { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<News> News { get; set; }


    }
}
