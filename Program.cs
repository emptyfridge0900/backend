using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<BookDbContext>(option=>{
    option.UseInMemoryDatabase("my_inmemory_db");
});
builder.Services.AddScoped<IBookService,BookService>();

var app = builder.Build();
app.UseMiddleware(typeof(ExceptionMiddleware));
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
