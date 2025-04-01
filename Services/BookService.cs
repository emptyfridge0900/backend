using System.ComponentModel;
using Microsoft.EntityFrameworkCore.Query;

public class BookService:IBookService{
    BookDbContext _db;
    public BookService(BookDbContext db)
    {
        _db=db;
    }

    public Book AddBook()
    {
        throw new NotImplementedException();
    }

    public Book DeleteBook()
    {
        throw new NotImplementedException();
    }

    public Book GetBook()
    {
        throw new NotImplementedException();
    }

    public List<Book> QueryBooks()
    {
        throw new NotImplementedException();
    }

    public Book UpdateBook()
    {
        throw new NotImplementedException();
    }
}