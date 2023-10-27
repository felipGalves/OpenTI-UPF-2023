using Library.API.Data;
using Library.API.Entities;
using Library.API.Persistence.Contracts;

namespace Library.API.Persistence.Repositories;

public class AuthorRepository : RepositoryBase<Author>, IAuthorRepository
{
    private readonly LibraryContext _conn;

    public AuthorRepository(LibraryContext conn) : base(conn)
    {
        _conn = conn;
    }

    public async Task AddRangeBooks(IEnumerable<Book> books)
    {
        if (books.Any()) 
        {
            _conn.Books.AddRange(books);
            await _conn.SaveChangesAsync();
        }
    }
}
