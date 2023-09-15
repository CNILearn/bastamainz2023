
using InstantAPIs.Generators.Helpers;

using InstantAPISample.Models;

using Microsoft.EntityFrameworkCore;

[assembly: InstantAPIsForDbContext(typeof(BooksContext))]

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BooksContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BooksConnection"));
});
// builder.Services.AddSqlServer<BooksContext>(builder.Configuration.GetConnectionString("BooksConnection"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapBooksContextToAPIs();

app.MapGet("/init", (BooksContext context) =>
{
    context.Database.EnsureCreated();
    return Results.Ok();
});

app.Run();
