using Catalog.API;
using Catalog.API.Products.DeleteProduct;
using Catalog.API.Products.UpdateProduct;

var builder = WebApplication.CreateBuilder(args);

//Add Services to Container

//Add Carter
builder.Services.AddCarter(new DependencyContextAssemblyCatalogCustom());

//Add Mediatr
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

//Add Fluent Validation
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

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

app.Run();
