using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TodoApi.Data;
using TodoApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Register DbContext and Swagger
builder.Services.AddDbContext<TodoContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Todo API", Version = "v1" });
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

if (app.Environment.IsDevelopment())

{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");

// ✅ GET: latest 5 uncompleted tasks
app.MapGet("/api/tasks", async (TodoContext db) =>
{
    var tasks = await db.Tasks
        .Where(t => !t.IsCompleted)
        .OrderByDescending(t => t.CreatedAt)
        .Take(5)
        .ToListAsync();

    return Results.Ok(tasks);
})
.WithName("GetTasks");

// ✅ POST: create a new task
app.MapPost("/api/tasks", async (TaskItem task, TodoContext db) =>
{
    if (task == null || string.IsNullOrWhiteSpace(task.Title))
        return Results.BadRequest("Task title is required.");

    task.CreatedAt = DateTime.UtcNow; // Explicitly set to the current UTC time

    db.Tasks.Add(task);
    await db.SaveChangesAsync();

    return Results.Created($"/api/tasks/{task.Id}", task);
})
.WithName("CreateTask");

// ✅ PATCH: mark a task as completed
app.MapPatch("/api/tasks/{id}/done", async (int id, TodoContext db) =>
{
    var task = await db.Tasks.FindAsync(id);
    if (task is null)
        return Results.NotFound();

    task.IsCompleted = true;
    await db.SaveChangesAsync();

    return Results.NoContent();
})
.WithName("MarkTaskDone");

// Root route
app.MapGet("/", () => "Todo API is running!");

app.Run();
