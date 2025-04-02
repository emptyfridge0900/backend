public record AddBookResponse(int id,string title,string author,int quantity, int totalSale){
    int Id {get;init;}=id;
    string Title{get;init;}=title;
    string Author{get;init;}=author;
    int Quantity{get;init;}=quantity;
    int TotalSale{get;init;}=totalSale;

}