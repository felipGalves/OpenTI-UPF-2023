namespace Library.API.Entities;

public class BaseEntity
{
    public BaseEntity()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }
    public DateTime DateAdded { get; set; }
    public DateTime? DateModified { get; set; }
}
