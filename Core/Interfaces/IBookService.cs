
public interface IBookService{
    public Task<Books> QueryBooks(int pageIndex, int itemPage, string? title, string? author);
    public Task<Book?> GetBook(int id);
    public Task<int> UpdateBook(int id, string? title, string? author, int quantity, int total);
    public Task<Book> AddBook(string? title, string? author);
    public Task<int> DeleteBook(int id);
}