using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

//Add Services for Dependency Injection

builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices(builder.Configuration);

var app = builder.Build();

//Add to Http Pipeline

app.UseApiServices();

if (app.Environment.IsDevelopment())
{
    app.InitializeMigrationAsync().GetAwaiter().GetResult();
}

app.Run();
