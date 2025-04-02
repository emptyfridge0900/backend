public class Book{
    public int Id{get;set;}
    public string? Title{get;private set;}
    public string? Author{get;private set;}
    public int Quantity{get;private set;}
    public int TotalSale {get;private set;}
    public Book(){}
    public Book(string? title, string? author, int quantity, int total)
    {
        SetTitle(title);
        SetAuthor(author);
        SetQuantity(quantity);
        SetTotalSale(total);
    }

    public void SetTitle(string? title){
        if(string.IsNullOrEmpty(title)){
            throw new UpdateRecordException("Title is not provided");
        }
        Title=title;
    }
    public void SetAuthor(string? author){
        if(string.IsNullOrEmpty(author)){
            throw new UpdateRecordException("Author is not provided");
        }
        Author=author;
    }
    public void SetQuantity(int qty){
        if(qty<0){
            throw new UpdateRecordException("Quantity cannot less than 0");
        }
        Quantity=qty;
    }
    public void SetTotalSale(int number){
        if(number<0){
            throw new UpdateRecordException("Sales number cannot less tha 0");
        }
        TotalSale =number;
    }
}