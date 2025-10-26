using LondonStockAPI;
using LondonStockAPI.Exceptions;
using Stocks.Application;
using Stocks.Infrastructure;
using Stocks.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration);

builder.Services.AddTransient<GlobalExceptionHandlerMiddleware>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.UseApiServices();

if(app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}
app.MapControllers();
app.Run();

