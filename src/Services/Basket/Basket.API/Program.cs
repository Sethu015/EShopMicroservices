using Basket.API;
using Basket.API.Basket.DeleteBasket;

var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;

//Adding Services

//Add Carter
builder.Services.AddCarter(new DependencyContextAssemblyCatalogCustom());

//Add Mediatr
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

//Add fluent validation
builder.Services.AddValidatorsFromAssembly(assembly);

//Mapster Configs
TypeAdapterConfig.GlobalSettings.Default.NameMatchingStrategy(NameMatchingStrategy.IgnoreCase);
TypeAdapterConfig.GlobalSettings.NewConfig<DeleteBasketResult, DeleteBasketResponse>().Map(dest => dest.isSuccess, src => src.isSuccess);

//Marten
builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
    opts.Schema.For<ShoppingCart>().Identity(x => x.UserName);
}).UseLightweightSessions();


var app = builder.Build();

//Configuring request pipeline

app.MapCarter();

app.Run();
