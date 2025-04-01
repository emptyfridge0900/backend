using Microsoft.EntityFrameworkCore;

public class BookDbContext:DbContext{
    public DbSet<Book> Books{get;set;}

    public BookDbContext(DbContextOptions<BookDbContext> option):base(option)
    {
        
    }
}