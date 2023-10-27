using Library.API.Data;
using Library.API.Entities;
using Library.API.Persistence.Contracts;

namespace Library.API.Persistence.Repositories;

public class BookRepository : RepositoryBase<Book>, IBookRepository
{
    public BookRepository(LibraryContext conn) : base(conn)
    {
    }
}
