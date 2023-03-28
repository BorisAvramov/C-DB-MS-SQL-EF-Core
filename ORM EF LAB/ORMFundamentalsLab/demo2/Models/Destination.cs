using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo2.Models
{
    public class Destination
    {

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int CategorryId { get; set; }

        public ICollection<Tourist>? Tourists { get; set; }

    }
}
