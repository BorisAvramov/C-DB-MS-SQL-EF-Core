namespace Advanced_Querying_LAB.Data.Models;

public class Album
{
    public Album()
    {
        Songs = new HashSet<Song>();
    }

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime ReleaseDate { get; set; }
    public int? ProducerId { get; set; }

    public virtual Producer? Producer { get; set; }
    public virtual ICollection<Song> Songs { get; set; }
}
