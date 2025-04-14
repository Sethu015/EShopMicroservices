using BuildingBlocks.Behaviors;
using Catalog.API;
using Catalog.API.Products.DeleteProduct;
using Catalog.API.Products.UpdateProduct;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

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

var app = builder.Build();

//Configure Http Request Pipeline

app.MapCarter();

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
