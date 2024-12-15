﻿using Microsoft.EntityFrameworkCore;
using DDB.HPApi.Data;
using DDB.HPApi.Repositories.Abstractions;
using DDB.HPApi.Repositories;
using DDB.HPApi.Services.Abstractions;
using DDB.HPApi.Services;
using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);

// Add database context
const string DB_NAME = "CharacterDB";
string connectionString = builder.Configuration.GetConnectionString(DB_NAME) ?? 
    throw new Exception($"Could not find connection string for data '{DB_NAME}'");
builder.Services.AddDbContext<CharacterContext>(options => options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddControllers();

// Add repositories and services
builder.Services.AddScoped<ICharacterRepository, CharacterRepository>();
builder.Services.AddScoped<ICharacterService, CharacterService>();

// Add memory cache
builder.Services.AddSingleton<IMemoryCache, MemoryCache>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Seed database data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<CharacterContext>();
    CharacterContextInitializer.Initialize(context);
}

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
