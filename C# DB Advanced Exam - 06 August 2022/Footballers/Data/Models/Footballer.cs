﻿//using Castle.Components.DictionaryAdapter;
using Footballers.Data.Models.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Footballers.Data.Models
{
    public class Footballer
    {


        public Footballer()
        {
            this.TeamsFootballers = new HashSet<TeamFootballer>();
        }

        [Key]
        public int Id  { get; set; }

        [Required]
        [MaxLength(ValidationsConstants.FootballerNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]

        public DateTime ContractStartDate  { get; set; }
        [Required]
        public DateTime ContractEndDate   { get; set; }

        [Required]
        public BestSkillType BestSkillType { get; set; }

        [Required]
        public PositionType PositionType { get; set; }

       

        [ForeignKey(nameof(Coach))]
        public int CoachId  { get; set; }

        public virtual Coach Coach { get; set; } = null!;


        public virtual ICollection<TeamFootballer> TeamsFootballers  { get; set; }

    }
}
