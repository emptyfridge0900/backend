using Microsoft.EntityFrameworkCore;

namespace BookStore.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddDbContext<BookDbContext>(option =>
            {
                option.UseInMemoryDatabase("my_inmemory_db");
            });
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "wwwroot"; // Matches myapp/wwwroot
            });
            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.MapStaticAssets();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.MapFallbackToFile("/index.html");
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "book.client";
            });
            app.Run();

        }
    }
}
