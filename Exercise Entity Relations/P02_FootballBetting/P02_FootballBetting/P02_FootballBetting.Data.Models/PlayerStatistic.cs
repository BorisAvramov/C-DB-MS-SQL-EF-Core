using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P02_FootballBetting.Data.Models
{
    public class PlayerStatistic
    {
        // HERE WE HAVE COMPOSITE PRIMARY KEY => WE WILL USE FLUENT API FOR CONFICUR IT

        [ForeignKey(nameof(Game))]
        public int GameId { get; set; }


        public virtual Game  Game { get; set; }

        [ForeignKey(nameof(Player))]
        public  int PlayerId { get; set; }

        public virtual  Player Player { get; set; }

        //WARNING : jUDJE MAY NOT BE HAPPY WITH BYTE
        public byte ScoredGoals { get; set; }

        public byte Assists { get; set; }

        public byte MinutesPlayed { get; set; }



    }
}
