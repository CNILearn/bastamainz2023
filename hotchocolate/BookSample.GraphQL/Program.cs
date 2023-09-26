using BookSample.Data.Database;
using BookSample.Data.Repositories;
using BookSample.GraphQL.GraphQL;
using BookSample.ReviewAPIClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using BookSample.Services.Books;
using BookSample.GraphQL.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPooledDbContextFactory<BookDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetRequired("Postgres:ConnectionString"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution);
});

builder.Services.AddTransient<IBookRepository, BookRepository>();
builder.Services.AddTransient<IBookService, BookService>();
builder.Services.AddTransient<IBookEventPublisher, BookSubscriptionSender>();

builder.Services.AddTransient<IRequestAdapter>(_ => new HttpClientRequestAdapter(new AnonymousAuthenticationProvider()));
builder.Services.AddTransient<ReviewsClient>();

builder.Services
    .AddGraphQLServer()
    .AddInMemorySubscriptions()
    .InitializeOnStartup()
    .SetPagingOptions(new()
    {
        IncludeTotalCount = true,
        DefaultPageSize = 100,
        MaxPageSize = 1000,
    })
    .AddMutationConventions()
    .AddProjections()
    .AddFiltering()
    .AddSorting()
    .AddGraphQLTypes()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>();

var app = builder.Build();

app.UseWebSockets();
app.MapGraphQL();

app.Run();
