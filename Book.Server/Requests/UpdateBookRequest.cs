public record UpdateBookRequest(){
    public string? Title{get;set;}
    public string? Author{get;set;}
    public int Quantity{get;set;}
    public int TotalSales{get;set;}
}