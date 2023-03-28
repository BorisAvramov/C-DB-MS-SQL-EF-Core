﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Footballers.Data.Models
{
    public class Coach
    {

        public Coach()
        {

            this.Footballers = new HashSet<Footballer>();
        }

        [Key]
        public int Id  { get; set; }

        [Required]
        [MaxLength(ValidationsConstants.CoachNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        public string Nationality { get; set; } = null!;

        public virtual ICollection<Footballer> Footballers  { get; set; }

    }
}