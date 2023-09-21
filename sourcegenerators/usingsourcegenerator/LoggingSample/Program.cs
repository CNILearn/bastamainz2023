using LoggingSample;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddTransient<Runner>();
using var host = builder.Build();

var runner = host.Services.GetRequiredService<Runner>();

runner.Run();