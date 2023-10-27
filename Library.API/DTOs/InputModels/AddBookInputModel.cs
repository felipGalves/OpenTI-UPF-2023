namespace Library.API.DTO;

public class AddBookInputModel
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Pages { get; set; }
    public string Category { get; set; } = string.Empty;
    public Guid AuthorId { get; set; }
}
