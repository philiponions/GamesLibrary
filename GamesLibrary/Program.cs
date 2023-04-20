using Microsoft.EntityFrameworkCore;
using GamesLibrary.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<GameContext>(options =>
    options.UseSqlite("Data Source=library.db"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Games Library");
});

app.UseAuthorization();

app.MapControllers();

app.Run();

