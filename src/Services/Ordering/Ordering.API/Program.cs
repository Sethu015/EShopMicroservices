var builder = WebApplication.CreateBuilder(args);

//Add Services for Dependency Injection

var app = builder.Build();

//Add to Http Pipeline

app.Run();
