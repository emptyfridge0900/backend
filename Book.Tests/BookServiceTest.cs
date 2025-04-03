using Microsoft.EntityFrameworkCore;
using Moq;


public class BookDbTest 
{
    [Fact]
    public async Task AddBookTest()
    {
        var mockSet = new Mock<DbSet<Book>>();
        var mockContext = new Mock<BookDbContext>();

        mockContext.Setup(db => db.Books).Returns(mockSet.Object);
        var service = new BookService(mockContext.Object);
        await service.AddBook("title1", "author1");
        mockSet.Verify(m => m.Add(It.IsAny<Book>()), Times.Once());
        mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
    }
    [Fact]
    public async Task GetAllBooksTest()
    {
        // Arrange
        var repositoryMock = new Mock<IBookService>();
        var books =new List<Book>(){new Book("title1","author1",0,0)};
        repositoryMock
            .Setup(r => r.QueryBooks(0,10,"title1","author1"))
            .ReturnsAsync(()=>new Books(books,1,0,10));

        var controller = new BookController(null,repositoryMock.Object);

        // Act
        var book = await controller.QueryBooks(new QueryBooks{PageIndex=0,ItemPage=10,Title="title1",Author="author1"});

        // Assert
        repositoryMock.Verify(r => r.QueryBooks(0,10,"title1","author1"));
        Assert.Equal(1, book.totalRecords);
    }

    [Fact]
    public async Task FindBookTest(){
        // Arrange
        var repositoryMock = new Mock<IBookService>();
        var b=new Book("title1","author1",0,1);
        b.Id=1;
        repositoryMock
            .Setup(r => r.GetBook(1))
            .Returns(Task.FromResult(b));

        var controller = new BookController(null,repositoryMock.Object);

        // Act
        var book = await controller.FindBook(1);

        // Assert
        repositoryMock.Verify(r => r.GetBook(1),Times.Once());
        Assert.Equal(1, book.id);
        Assert.Equal("author1", book.author);
        Assert.Equal("title1", book.title);
        Assert.Equal(0, book.quantity);
        Assert.Equal(1, book.totalSales);
    }

    [Fact]
    public async Task DeleteBookTest(){
        // Arrange
        var repositoryMock = new Mock<IBookService>();

        repositoryMock
            .Setup(r => r.DeleteBook(1));

        var controller = new BookController(null,repositoryMock.Object);

        // Act
        var book = await controller.DeleteBook(1);

        // Assert
        repositoryMock.Verify(r => r.DeleteBook(1),Times.Once());
    }
    [Fact]
    public async Task UpdateBookTest(){
        // Arrange
        var repositoryMock = new Mock<IBookService>();

        repositoryMock
            .Setup(r => r.UpdateBook(1,"title2","author2",1,2))
            .Returns(Task.FromResult(1));

        var controller = new BookController(null,repositoryMock.Object);

        // Act
        var response = await controller.UpdateBook(1,new UpdateBookRequest{Title="title2",Author="author2",Quantity=1,TotalSales=1});

        // Assert
        repositoryMock.Verify(r => r.UpdateBook(1,"title2","author2",1,1),Times.Once());
    }


    [Fact]
    public async Task FindBookExceptionTest(){
        // Arrange
        var repositoryMock = new Mock<IBookService>();
        var b=new Book("title1","author1",0,1);
        b.Id=1;
        repositoryMock
            .Setup(r => r.GetBook(1))
            .Returns(Task.FromResult(b));

        var controller = new BookController(null,repositoryMock.Object);

        // Act
        var action = () =>  controller.FindBook(2);
        
        // Assert
        var exception = Assert.ThrowsAsync<RecordNotFoundException>(action);
        Assert.Equal("Record not found", (await exception).Message);
        
    }


}
