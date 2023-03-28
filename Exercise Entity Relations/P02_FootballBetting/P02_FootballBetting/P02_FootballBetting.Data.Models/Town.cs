using P02_FootballBetting.Data.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P02_FootballBetting.Data.Models
{
    public class Town
    {
        public Town()
        {
            this.Teams= new List<Team>();
        }


        [Key]
        public int TownId { get; set; }

        [Required]
        [MaxLength(ValidationConstants.TownNameMaxLength)]
        public string Name { get; set; }

        [ForeignKey(nameof(Country))]
        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        //TODO: create navigations properties


        public virtual ICollection<Team> Teams { get; set; }



    }
}
