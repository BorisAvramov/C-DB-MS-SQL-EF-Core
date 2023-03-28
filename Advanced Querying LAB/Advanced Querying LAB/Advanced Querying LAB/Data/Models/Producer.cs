namespace Advanced_Querying_LAB.Data.Models;

public class Producer
{
    public Producer()
    {
        Albums = new HashSet<Album>();
    }

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Pseudonym { get; set; }
    public string? PhoneNumber { get; set; }

    public virtual ICollection<Album> Albums { get; set; }
}
