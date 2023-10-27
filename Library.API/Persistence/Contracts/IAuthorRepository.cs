using Library.API.Entities;

namespace Library.API.Persistence.Contracts;

public interface IAuthorRepository : IRepositoryBase<Author>
{
    Task AddRangeBooks(IEnumerable<Book> books);
}
