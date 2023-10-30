using AutoMapper;
using Library.API.DTO;
using Library.API.Entities;
using Library.API.Persistence.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers;

[ApiController]
[Route("v1/[controller]")]

public class BookController : ControllerBase
{
    private readonly IBookRepository _book;
    private readonly IMapper _mapper;

    public BookController(IBookRepository book, IMapper mapper)
    {
        _book = book;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<dynamic>> AddBook([FromBody] AddBookInputModel model) 
    {
        await _book.AddAsync(_mapper.Map<Book>(model));

        return Ok("Livro cadastrado com sucesso!");
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<dynamic>> UpdateBook([FromBody] UpdateBookInputModel model) 
    {
        await _book.UpdateAsync(_mapper.Map<Book>(model));

        return Ok("Livro atualizado com sucesso!");
    }

    [HttpGet]
    public async Task<ActionResult<dynamic>> GetBooks([FromQuery] FiltersBookInputModel filters) 
    {
        return Ok(await _book.GetAllAsync(x => _mapper.Map<GetBookViewModel>(x), x => x.Author, filters));
    }
    

    [HttpGet("{id}")]
    public async Task<ActionResult<dynamic>> GetSingleBook(Guid id) 
    {
        return Ok(_mapper.Map<GetSingleBookViewModel>(await _book.GetSingleAsync(id, inc => inc.Author)));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<dynamic>> RemoveBook(Guid id) 
    {
        await _book.RemoveByIdAsync(id);

        return Ok("Livro excluido com sucesso!");
    }
}
