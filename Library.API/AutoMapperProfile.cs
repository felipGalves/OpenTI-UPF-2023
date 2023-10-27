using AutoMapper;
using Library.API.DTO;
using Library.API.Entities;

namespace Library.API;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // add Author
        CreateMap<AddAuthorInputModel, Author>().ReverseMap();
        CreateMap<UpdateAuthorInputModel, Author>().ReverseMap();

        // get Authors
        CreateMap<GetAuthorsViewModel, Author>().ReverseMap();
        CreateMap<IEnumerable<Author>, IEnumerable<GetAuthorsViewModel>>().ReverseMap();

        //add book
        CreateMap<AddBookInputModel, Book>().ReverseMap();
        CreateMap<Book, GetBookViewModel>().ForMember(x => x.AuthorName, b => b.MapFrom(s => s.Author.Name));

        CreateMap<Book, GetSingleBookViewModel>().ForMember(x => x.Author, b => b.MapFrom(s => new GetSingleAuthorViewModel() {
            Description = s.Author.Description,
            Name = s.Author.Name
        }));

        CreateMap<AddBookAuthorInputModel, Book>().ForMember(dest => dest.AuthorId, opt => opt.Ignore());
    }
}
