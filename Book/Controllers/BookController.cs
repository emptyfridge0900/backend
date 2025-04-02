using Microsoft.AspNetCore.Mvc;
namespace backend.Controllers;

[ApiController]
[Route("api/books")]
public class BookController : ControllerBase
{
    private readonly ILogger<BookController> _logger;
    private readonly IBookService _bookServ;
    public BookController(ILogger<BookController> logger,IBookService bookServ)
    {
        _logger = logger;
        _bookServ = bookServ;
    }

    [HttpGet]
    [ResponseCache(Duration = 120, Location = ResponseCacheLocation.Any, NoStore = false)]
    public async Task<QueryBooksResponse> QueryBooks([FromQuery]QueryBooks rst){
        var books = await _bookServ.QueryBooks(rst.PageIndex, rst.ItemPage, rst.Title, rst.Author);

        return new QueryBooksResponse(books.result,books.totalRecords,books.result.Count);
    }
    [HttpGet("{id}")]
    [ResponseCache(Duration = 120, Location = ResponseCacheLocation.Any, NoStore = false)]
    public async Task<FindBookResponse> FindBook(int id){
        var book = await _bookServ.GetBook(id);
        if(book==null){
            throw new RecordNotFoundException("Record not found");
        }
        return new FindBookResponse(book.Id,book.Title!,book.Author!,book.Quantity,book.TotalSale);
    }
    [HttpPost]
    public async Task<AddBookResponse> AddBook([FromBody]AddBookRequest rst){
        var book = await _bookServ.AddBook(rst.Title, rst.Author);
        return new AddBookResponse(book.Id,book.Title!,book.Author!,book.Quantity,book.TotalSale);
    }
    [HttpPut("{id}")]
    public async Task<UpdateBookResponse> UpdateBook([FromRoute]int id, [FromBody] UpdateBookRequest rst){
        var changed = await _bookServ.UpdateBook(id, rst.Title, rst.Author, rst.Quantity, rst.Total);
        return new UpdateBookResponse(changed);
    }
    [HttpDelete("{id}")]
    public async Task<DeleteBookResponse> DeleteBook(int id){
        var deletedId =await _bookServ.DeleteBook(id);
        return new DeleteBookResponse(deletedId);
    }
}





