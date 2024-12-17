using Microsoft.EntityFrameworkCore;
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

// Allow requests from the front end
string _corsPolicyName = "ddb-hp-ui";
builder.Services.AddCors(options =>
{
    options.AddPolicy(_corsPolicyName, policybuilder =>
    {
        policybuilder.WithOrigins("http://localhost:3000");
        policybuilder.AllowAnyHeader(); // In the future we would want to be more specific about the headers allowed
        policybuilder.AllowAnyMethod(); // In the future we would want to be more specific about the methods allowed
    });
});

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

app.UseCors(_corsPolicyName);

app.UseAuthorization();

app.MapControllers();

app.Run();
