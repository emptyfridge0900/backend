
public interface IBookService{
    public List<Book> QueryBooks();
    public Book GetBook();
    public Book UpdateBook();
    public Book AddBook();
    public Book DeleteBook();
}