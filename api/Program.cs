using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using LiteDB;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Register LiteDB as singleton
builder.Services.AddSingleton<LiteDatabase>(_ => new LiteDatabase("LegalCases.db"));

// Register repository (depends on LiteDB)
builder.Services.AddSingleton<CaseRepository>();

// Add controllers
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Map controllers
app.MapControllers();

app.Run();
