namespace Library.API.Entities;

public class Book : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Pages { get; set; }
    public string Category { get; set; } = string.Empty;

    public Author? Author { get; set; }
    public Guid AuthorId { get; set; }
}
