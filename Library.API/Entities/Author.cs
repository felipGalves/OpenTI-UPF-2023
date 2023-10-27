using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.API.Entities;

public class Author : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public ICollection<Book> Books { get; set; } = new Collection<Book>();

    internal class AuthorMap : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasMany(x => x.Books)
                   .WithOne(y => y.Author)
                   .HasForeignKey(f => f.AuthorId);
        }
    }
}
