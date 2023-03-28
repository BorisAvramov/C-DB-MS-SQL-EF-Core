using Microsoft.EntityFrameworkCore.Metadata.Internal;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using MusicHub.Data.Models.Enums;

namespace MusicHub.Data.Models;

public class Song
{
    public Song()
    {
        this.SongPerformers = new HashSet<SongPerformer>();
    }

    [Key]
    public int Id { get; set; }

    [MaxLength(ValidateConstants.songNameMaxLength)]
    public string Name { get; set; } = null!;

    // in db will be stored as BIGINT
    //required by default
    public TimeSpan Duration { get; set; }

    [Column(TypeName = "date")]
    public DateTime CreatedOn { get; set; }

    // enumerators are stored in db as int
    public Genre Genre { get; set; }

    [ForeignKey(nameof(Album))]
    public int? AlbumId { get; set; }

    public virtual Album? Album { get; set; }

    [ForeignKey(nameof(Writer))]
    public int WriterId { get; set; }

    public virtual Writer Writer { get; set; } = null!;


    public decimal Price { get; set; }


    public virtual  ICollection<SongPerformer> SongPerformers { get; set; }

}
