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
    public class User
    {

        public User()
        {
            this.Bets = new HashSet<Bet>();
        }

        // in real project must be string instead of int in order to provide security
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(ValidationConstants.UserNameMaxLength)]
        public string Username { get; set; }

        //Password are saved hashed in  the DB 
        // algoritm for hashing is SA
        [Required]
        [MaxLength(ValidationConstants.PasswordMaxLength)]
        public string Password { get; set; }

        [Required]
        [MaxLength(ValidationConstants.EmailMaxLength)]
        public string Email { get; set; }

        [Required]
        [MaxLength(ValidationConstants.UserNameMaxLength)]
        public string Name { get; set; }

        [Column(TypeName ="money")]
        public decimal Balance { get; set; }


        public virtual ICollection<Bet> Bets { get; set; }


    }
}
