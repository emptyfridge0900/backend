using Microsoft.EntityFrameworkCore;

public class BookDbContext:DbContext{
    public virtual DbSet<Book> Books{get;set;}

    public BookDbContext(){}
    public BookDbContext(DbContextOptions<BookDbContext> option):base(option)
    {
        
    }
}