namespace Advanced_Querying_LAB.Data.Models;

public class Song
{
    public Song()
    {
        Performers = new HashSet<Performer>();
    }

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public TimeSpan Duration { get; set; }
    public DateTime CreatedOn { get; set; }
    public int Genre { get; set; }
    public int? AlbumId { get; set; }
    public int WriterId { get; set; }
    public decimal Price { get; set; }

    public virtual Album? Album { get; set; }
    public virtual Writer Writer { get; set; } = null!;

    public virtual ICollection<Performer> Performers { get; set; }
}
