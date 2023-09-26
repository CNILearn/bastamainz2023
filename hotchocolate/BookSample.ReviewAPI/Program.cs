using BookSample.ReviewAPI.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    setup.AddServer(new(){
        Url = "https://localhost:7078"
    });
});

builder.Services.AddSingleton<ReviewRepository>();

var app = builder.Build();

app.MapGet("/api/reviews", ([FromQuery] string[] bookIds, [FromQuery] int? take, [FromServices] ReviewRepository repo) =>
{
    // Using string[] instead of long[] for the bookIds, because Kiota (used to generate the API-client) has a bug (#3354).
    return repo.GetReviews(bookIds.ParseLongsSafely(), take);
})
.WithTags("Reviews")
.WithName("GetReviews")
.WithOpenApi();

app.MapGet("/api/ratings", ([FromQuery] string[] bookIds, [FromServices] ReviewRepository repo) =>
{
    // Using string[] instead of long[] for the bookIds, because Kiota (used to generate the API-client) has a bug (#3354).
    return repo.GetRatings(bookIds.ParseLongsSafely());
})
.WithTags("Ratings")
.WithName("GetRatings")
.WithOpenApi();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
