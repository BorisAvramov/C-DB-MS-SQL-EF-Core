using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace P01_StudentSystem.Data.Models
{
    public class Homework
    {
        public int HomeworkId { get; set; }

        [Column(TypeName ="varchar(255)")]
        public string Content { get; set; }

        public ContentType ContentType { get; set; }

        public DateTime SubmissionTime { get; set; }

        public int StudentId { get; set; }

        public virtual Student Student { get; set; }
 
        public int CourseId { get; set; }

        public virtual Course Course { get; set; }

        //        o HomeworkId
        //o Content – string, linking to a file, not unicode
        //o   ContentType - enum, can be Application, Pdf or Zip
        //o   SubmissionTime
        //o   StudentId
        //o   CourseId

    }
}
