var builder = WebApplication.CreateBuilder(args);

//Add Services to Container

var app = builder.Build();

//Configure Http Request Pipeline

app.Run();
