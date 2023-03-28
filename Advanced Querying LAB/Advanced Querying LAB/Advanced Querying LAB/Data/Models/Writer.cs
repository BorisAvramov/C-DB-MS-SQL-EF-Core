namespace Advanced_Querying_LAB.Data.Models;

public class Writer
{
    public Writer()
    {
        Songs = new HashSet<Song>();
    }

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Pseudonym { get; set; }

    public virtual ICollection<Song> Songs { get; set; }
}
