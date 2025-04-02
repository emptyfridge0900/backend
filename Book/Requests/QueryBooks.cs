using System.ComponentModel;

public record QueryBooks(){
    [DefaultValue(0)]
    public int PageIndex = 0;
    [DefaultValue(10)]
    public int ItemPage =10;
    public string? Title{get;set;}
    public string? Author{get;set;}
}