using Catalog.API;
using Catalog.API.Products.DeleteProduct;
using Catalog.API.Products.UpdateProduct;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

//Add Services to Container

//Add Carter
builder.Services.AddCarter(new DependencyContextAssemblyCatalogCustom());

var assembly = typeof(Program).Assembly;
//Add Mediatr
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

//Add Fluent Validation
builder.Services.AddValidatorsFromAssembly(assembly);

//Mapster Config
TypeAdapterConfig.GlobalSettings.Default.NameMatchingStrategy(NameMatchingStrategy.IgnoreCase);
TypeAdapterConfig.GlobalSettings.NewConfig<UpdateProductsResult, UpdateProductResponse>().Map(dest => dest.isSuccess, src => src.isSuccess);
TypeAdapterConfig.GlobalSettings.NewConfig<DeleteProductResult, DeleteProductResponse>().Map(dest => dest.isSuccess, src => src.isSuccess);

//Marten Register
builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

if (builder.Environment.IsDevelopment())
    builder.Services.InitializeMartenWith<CatalogInitialData>();

//Add Exception handler
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

//Add health checks

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

var app = builder.Build();

//Configure Http Request Pipeline

app.MapCarter();

app.UseExceptionHandler(options => { });

app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

//Custom Exception - Commented as we use global exception handling
//app.UseExceptionHandler(handler =>
//{
//    handler.Run(async context =>
//    {
//        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
//        if (exception == null)
//        {
//            return;
//        }

//        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
//        logger.LogError(exception, exception.Message);

//        var problemDetails = new ProblemDetails
//        {
//            Title = exception.Message,
//            Status = StatusCodes.Status500InternalServerError,
//            Detail = exception.StackTrace
//        };

//        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
//        context.Response.ContentType = "application/problem+json";

//        await context.Response.WriteAsJsonAsync(problemDetails);

//    });
//});

app.Run();
