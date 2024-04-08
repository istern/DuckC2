using DuckC2.Server.Abstractions.Factories;
using DuckC2.Server.Abstractions.Models;
using DuckC2.Server.Abstractions.Repositories;
using DuckC2.Server.Factories;
using DuckC2.Server.Models;
using DuckC2.Server.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton<IListenerRepository, InMemoryListenerRepository>();
builder.Services.AddSingleton<IListenerFactory, HttpListenerFactory>();


var app = builder.Build();

app.MapControllers();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.Run();
