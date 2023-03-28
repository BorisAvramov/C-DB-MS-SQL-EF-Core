using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace P01_StudentSystem.Data.Models
{
    public class Resource
    {

        public int ResourceId { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [Column(TypeName ="varchar(2048)")]
        public string Url { get; set; }


        public ResourceType ResourceType { get; set; }

        public int CourseId { get; set; }

        public virtual Course Course { get; set; }

        //        o ResourceId
        //o Name – up to 50 characters, unicode
        //o   Url – not unicode
        //o ResourceType – enum, can be Video, Presentation, Document or Other
        //o   CourseId


    }
}
