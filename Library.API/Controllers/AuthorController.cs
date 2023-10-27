using AutoMapper;
using Library.API.DTO;
using Library.API.Entities;
using Library.API.Persistence.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class AuthorController : ControllerBase
{
    private readonly IAuthorRepository _author;
    private readonly IMapper _mapper;

    public AuthorController(
        IAuthorRepository author,
        IMapper mapper)
    {
        _author = author;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<dynamic>> AddAuthor([FromBody] AddAuthorInputModel model) 
    {
        await _author.AddAsync(_mapper.Map<Author>(model));

        return Ok("Autor cadastrado com sucesso!");
    }

    [HttpPost("{id}/books")]
    public async Task<ActionResult<dynamic>> AddBooksByAuthor([FromBody] List<AddBookAuthorInputModel> model, Guid id) 
    {
        IEnumerable<Book> books = model.Select(x => _mapper.Map<Book>(x, opt => opt.AfterMap((src, dest) => dest.AuthorId = id))).ToList();

        await _author.AddRangeBooks(books);

        return Ok("Livros cadastrados com sucesso!");
    }
    
    [HttpPut]
    public async Task<ActionResult<dynamic>> UpdateAuthor([FromBody] UpdateAuthorInputModel model) 
    {
        await _author.UpdateAsync(_mapper.Map<Author>(model));

        return Ok("Autor atualizado com sucesso!");
    }

    [HttpGet]
    public async Task<ActionResult<dynamic>> GetAuthors() 
    {
        return Ok(await _author.GetAllAsync(x => _mapper.Map<GetAuthorsViewModel>(x)));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<dynamic>> GetSingleAuthor(Guid id) 
    {
        return Ok(_mapper.Map<GetAuthorsViewModel>(await _author.GetSingleAsync(id)));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<dynamic>> RemoveAuthor(Guid id) 
    {
        await _author.RemoveByIdAsync(id);

        return Ok("Autor excluido com sucesso!");
    }
}
