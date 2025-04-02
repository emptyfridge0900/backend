public record Books(List<Book> result, int totalRecords, int pageIndex, int itemPage)
{
    public List<Book> Result { get; } = result;
    public int TotalRecords { get; } = totalRecords;
    public int PageIndex { get; } = pageIndex;
    public int ItemPage { get; } = itemPage;
}