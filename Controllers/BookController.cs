using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace backend.Controllers;

[ApiController]
[Route("api/books")]
public class BookController : ControllerBase
{
    private readonly ILogger<BookController> _logger;
    private readonly BookService _bookServ;
    public BookController(ILogger<BookController> logger,BookService bookServ)
    {
        _logger = logger;
        _bookServ = bookServ;
    }

    [HttpGet]
    public IResult GetBooks(){
        return Results.Ok(new {});
    }
    [HttpGet("{id}")]
    public IResult GetBook(){
        
        return Results.Ok(new {});
    }
    [HttpPost]
    public IResult AddBook(){
        
        return Results.Ok(new {});
    }
    [HttpPut("{id}")]
    public IResult UpdateBook(){
        
        return Results.Ok(new {});
    }
    [HttpDelete("{id}")]
    public IResult DeleteBook(){
        
        return Results.Ok(new {});
    }
}
