using P02_FootballBetting.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P02_FootballBetting.Data.Models
{
    public class Bet
    {
        [Key]

       // it is much better to be GUID, not int
       // with int someone could hack the bet when search bet by id for exemple bet with id = 328
        public int BetId { get; set; }

        [Column(TypeName ="money")]
        public decimal Amount { get; set; }

        //Enumerators are stored as integer (non - nullable )
        //required by default
        public Prediction Prediction { get; set; }

        public DateTime DateTime { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        [ForeignKey(nameof(Game))]
        public int GameId { get; set; }

        public virtual Game Game { get; set; }


    }
}
