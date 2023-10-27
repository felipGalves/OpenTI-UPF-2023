namespace Library.API;

public class GetSingleBookViewModel
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Pages { get; set; }
    public string Category { get; set; } = string.Empty;

    public GetSingleAuthorViewModel Author { get; set; } = new();
}
