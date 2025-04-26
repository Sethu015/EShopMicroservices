var builder = WebApplication.CreateBuilder(args);

//Adding Services

var app = builder.Build();

//Configuring request pipeline

app.Run();
