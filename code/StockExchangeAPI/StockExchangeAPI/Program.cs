using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using Repository.RepoClasses;
using StockExchangeAPI.Interfaces;
using StockExchangeAPI.Services;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Inject the dependencies
//!Connection string should be stored in Key Vault
builder.Services.AddDbContext<DevelopmentContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<ILoggerService, LoggerService>();
builder.Services.AddScoped<IStockService, StockService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

//Also add custom middle ware for Authentication 
//Add custom middle ware for production level logging like splunk
//Add Rate limiting middlw ware

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
