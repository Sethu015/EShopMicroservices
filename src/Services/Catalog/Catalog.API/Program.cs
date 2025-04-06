using Catalog.API;

var builder = WebApplication.CreateBuilder(args);

//Add Services to Container

//Add Carter
builder.Services.AddCarter(new DependencyContextAssemblyCatalogCustom());

//Add Mediatr
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

//Mapster Config
TypeAdapterConfig.GlobalSettings.Default.NameMatchingStrategy(NameMatchingStrategy.IgnoreCase);

//Marten Register
builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

var app = builder.Build();

//Configure Http Request Pipeline

app.MapCarter();

app.Run();
