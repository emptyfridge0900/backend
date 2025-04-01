using Microsoft.EntityFrameworkCore;

public class BookService:IBookService{
    BookDbContext _db;
    public BookService(BookDbContext db)
    {
        _db=db;
    }

    public async Task<Book> AddBook(string? title, string? author)
    {
        var book = new Book();
        book.SetTitle(title);
        book.SetAuthor(author);
        book.SetQuantity(0);
        book.SetTotalSale(0);
        _db.Books.Add(book);
        await _db.SaveChangesAsync();

        return book;
    }

    public async Task<int> DeleteBook(int id)
    {
        var book = await _db.Books.SingleOrDefaultAsync(i=>i.Id==id);
        if(book!=null){
            _db.Books.Remove(book);
        }
        else{
            throw new RecordNotFoundException("Record not found");
        }
        await _db.SaveChangesAsync();
        return id;
    }

    public Task<Book?> GetBook(int id)
    {
        return _db.Books.SingleOrDefaultAsync(i=>i.Id==id);
    }

    public async Task<Books> QueryBooks(int pageIndex, int itemPage, string? title, string? author)
    {
        if (itemPage == 0)
        {
            itemPage = int.MaxValue;
        }
        var totalRecords = _db.Books.Count();
        var  books = await _db.Books
            .Where(i => String.IsNullOrEmpty(title) || i.Title!.Contains(title))
            .Where(i => string.IsNullOrEmpty(author) || i.Author!.Contains(author))
            .Skip(pageIndex*itemPage).Take(itemPage).ToListAsync();
        return new Books(books, totalRecords,pageIndex,itemPage);
        
    }

    public async Task<int> UpdateBook(int id, string? title, string? author, int quantity, int total)
    {
        var book = await _db.Books.SingleOrDefaultAsync(i=>i.Id==id);
        if(book!=null){
            _db.Books.Remove(book);
        }
        else{
            throw new RecordNotFoundException("Record not found");
        }

        book.SetTitle(title);
        book.SetAuthor(author);
        book.SetQuantity(quantity);
        book.SetTotalSale(total);

        _db.Books.Update(book);
        return await _db.SaveChangesAsync();
    }


}