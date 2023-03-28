namespace Advanced_Querying_LAB.Data.Models;

public class Performer
{
    public Performer()
    {
        Songs = new HashSet<Song>();
    }

    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public int Age { get; set; }
    public decimal NetWorth { get; set; }

    public virtual ICollection<Song> Songs { get; set; }
}
