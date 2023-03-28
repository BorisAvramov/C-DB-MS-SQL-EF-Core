using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P01_StudentSystem.Data.Models
{
    public class Course
    {

        public Course()
        {
            this.Homeworks = new HashSet<Homework>();
            this.StudentsCourses = new HashSet<StudentCourse>();
            this.Resources = new HashSet<Resource>();
        }

        public int CourseId { get; set; }

        [MaxLength(80)]
        public string Name { get; set; }

        public string? Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal Price { get; set; }

        public virtual ICollection<Homework> Homeworks { get; set; }

        public virtual ICollection<StudentCourse> StudentsCourses { get; set; }

        public virtual ICollection<Resource> Resources { get; set; }


        //        o CourseId
        //o Name – up to 80 characters, unicode
        //o   Description – unicode, not required
        //o StartDate
        //o EndDate
        //o Price

    }
}
