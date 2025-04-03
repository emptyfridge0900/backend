using System.ComponentModel;

public record QueryBooks(){
    [DefaultValue(0)]
    public int PageIndex {get;set;}
    [DefaultValue(10)]
    public int ItemPage {get;set;}
    public string? Title{get;set;}
    public string? Author{get;set;}
}