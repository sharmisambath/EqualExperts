using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using ShoppingCart.Models;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();
builder.Services.Configure<AppSettings>(config.GetSection("AppSettings"));

Console.WriteLine(config.GetSection("AppSettings").GetSection("PriceApiServiceSettings"));

builder.Services.AddHttpClient("priceapiclient", c =>
{
    c.BaseAddress = new Uri(config.GetSection("AppSettings")?.GetSection("PriceApiServiceSettings")?
                            .GetValue<string>("BaseUrl") ?? "");
    c.Timeout = TimeSpan.FromSeconds(15);
    c.DefaultRequestHeaders.Add(
        "accept", "application/json");
})
.ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler()
{
    PooledConnectionIdleTimeout = TimeSpan.FromMinutes(5)
})
.SetHandlerLifetime(TimeSpan.FromMinutes(20));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();

