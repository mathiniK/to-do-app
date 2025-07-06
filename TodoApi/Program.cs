using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TodoApi.Data;
using TodoApi.Services;
using TodoApi.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Configure Services
builder.Services.AddDbContext<TodoContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Todo API",
        Version = "v1"
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

if (!app.Environment.IsEnvironment("Docker"))
{
    app.UseHttpsRedirection();
}

// Apply Database Migration
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TodoContext>();
    if (db.Database.IsRelational())
    {
        db.Database.Migrate();
    }
}

// Map Endpoints
app.MapTaskEndpoints(); 
app.MapGet("/", () => "Todo API is running!");

app.Run();
public partial class Program { }
