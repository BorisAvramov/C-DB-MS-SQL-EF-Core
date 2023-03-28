using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P01_StudentSystem.Data.Models
{
    public class Student
    {
        public Student()
        {
            this.StudentsCourses = new HashSet<StudentCourse>();
            this.Homeworks = new HashSet<Homework>();
        }


        public int StudentId { get; set; }

        [MaxLength(100)]
        [Unicode(true)]
        public string Name { get; set; }

        [Column(TypeName = "char(10)")]
        public string? PhoneNumber  { get; set; }

        public DateTime RegisteredOn { get; set; }

        public DateTime? Birthday  { get; set; }


        public virtual ICollection<StudentCourse> StudentsCourses { get; set; }

        public virtual ICollection<Homework> Homeworks { get; set; }



        //        o StudentId
        //o Name – up to 100 characters, unicode
        //o   PhoneNumber – exactly 10 characters, not unicode, not required
        //o RegisteredOn
        //o Birthday – not required

    }
}
