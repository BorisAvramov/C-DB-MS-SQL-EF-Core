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
    public class Player
    {
        public Player()
        {
            this.PlayersStatistics = new HashSet<PlayerStatistic>();
        }

        [Key]
        public int PlayerId { get; set; }

        [Required]
        [MaxLength(ValidationConstants.PlayerMaxLengthName)]
        public string Name { get; set; }

        public int SquadNumber { get; set; }

        //sql type is bit
        //by default  bool is not null  => BY DEFAULT IS REQUIRED
        public bool IsInjured { get; set; }

        //this fk should be nullable
        //WARNING: THIS MAY CAUSE AN ERROR IN JUDJE

        [ForeignKey(nameof(Team))]
        public int TeamId { get; set; } 


        public virtual Team Team { get; set; }

        [ForeignKey(nameof(Position))]
        public int PositionId { get; set; }

        public virtual Position Position { get; set; }


        public virtual ICollection<PlayerStatistic> PlayersStatistics { get; set; }

    }
}
