public record QueryBooksResponse(List<Book> results, int totalRecords, int numberOfRecords){
    List<Book> Results{get;init;} = results;
    int TotalRecords{get;init;} = totalRecords;
    int NumOfRecords{get;init;} = numberOfRecords;
}