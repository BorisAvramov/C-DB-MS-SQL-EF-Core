using Microsoft.EntityFrameworkCore;
using P01_StudentSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P01_StudentSystem.Data
{
    public class StudentSystemContext : DbContext
    {
        // create ctor
        // create dbsets
        //connect sql server
        // fluent api relation - many to many
        public StudentSystemContext()
        {

        }

        public StudentSystemContext(DbContextOptions options)
            :base(options)
        {

        }

         public DbSet<Course> Courses { get; set; }
         public DbSet<Homework> Homeworks { get; set; }
         public DbSet<Resource> Resources { get; set; }
         public DbSet<Student> Students { get; set; }
         public DbSet<StudentCourse> StudentsCourses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-6ADIQGK\\SQLEXPRESS;Database=StudentSystem;Integrated Security=True");

            }


            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourse>().HasKey(c =>  new {c.StudentId, c.CourseId });

            modelBuilder.Entity<StudentCourse>(entity =>
            {

                entity.HasOne(s => s.Student)
                       .WithMany(c => c.StudentsCourses)
                       .HasForeignKey(s => s.StudentId);
                entity.HasOne(c => c.Course)
                      .WithMany(s => s.StudentsCourses)
                      .HasForeignKey(c => c.CourseId);



            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
