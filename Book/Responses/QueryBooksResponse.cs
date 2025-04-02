public record QueryBooksResponse(List<Book> result, int totalRecord, int numberOfRecords){
    List<Book> Results{get;init;} = result;
    int TotalRecords{get;init;} = totalRecord;
    int NumOfRecords{get;init;} = numberOfRecords;
}