using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Footballers.Data.Models
{
    public class Team
    {

        public Team()
        {
            this.TeamsFootballers = new HashSet<TeamFootballer>();
        }

        [Key]
        public int Id  { get; set; }

        [Required]
        [MaxLength(ValidationsConstants.TeamNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(ValidationsConstants.NationalityMaxLength)]
        public string Nationality { get; set; } = null!;

        [Required]
        public int Trophies  { get; set; }


        public virtual ICollection<TeamFootballer> TeamsFootballers  { get; set; }




    }
}
