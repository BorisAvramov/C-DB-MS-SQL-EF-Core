using P02_FootballBetting.Data.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P02_FootballBetting.Data.Models
{
    public  class Team
    {
        public Team()
        {
            this.HomeGames = new HashSet<Game>();
            this.AwayGames = new HashSet<Game>();
            this.Players = new HashSet<Player>();

        }


        [Key]
        public int TeamId { get; set; }

        [Required]
        [MaxLength(ValidationConstants.TeamNameMaxLength)]
        public string Name { get; set; }

        [MaxLength(ValidationConstants.TeamLogoUrlMAxLength)]
        public string? LogoUrl { get; set; }

        [Required]
        [MaxLength(ValidationConstants.TeamInitialMxLength)]
        public string Initials { get; set; }

        //required bu d3efault (NOT NULL) because decimal data type is nopt nullable
        [Column(TypeName ="money")]
        public decimal Budget { get; set; }


        // TODO SET RELATIONS
        [ForeignKey(nameof(PrimaryKitColor))]
        public int PrimaryKitColorId { get; set; }

        public virtual Color PrimaryKitColor { get; set; }


        [ForeignKey(nameof(SecondaryKitColor))]
        public int SecondaryKitColorId { get; set; }

        public virtual Color SecondaryKitColor { get; set; }

        [ForeignKey(nameof(Town))]
        public int TownId { get; set; }

        public virtual Town Town { get; set; }


        [InverseProperty("HomeTeam")]
        public virtual ICollection<Game> HomeGames { get; set; }
          

        [InverseProperty("AwayTeam")]
        public virtual ICollection<Game> AwayGames { get; set; }


        public virtual ICollection<Player> Players { get; set; }
    }
}