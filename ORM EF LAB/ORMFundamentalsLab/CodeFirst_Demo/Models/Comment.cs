using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst_Demo.Models
{
    public class Comment
    {
        public int Id { get; set; }
            
        public int? NewsId { get; set; }

        public News? News { get; set; }

        [MaxLength(50)]
        public string? Authow { get; set; }

        [Required]
        public string Content { get; set; }

    }
}
