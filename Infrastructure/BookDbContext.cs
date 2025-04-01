using Microsoft.EntityFrameworkCore;

public class BookDbContext:DbContext{
    Book Book{get;set;}
}