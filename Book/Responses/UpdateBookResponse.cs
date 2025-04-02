public record UpdateBookResponse(int changed){
    int ChangedEntity {get;init;} =changed;
}