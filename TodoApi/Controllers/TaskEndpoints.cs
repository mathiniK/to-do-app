using TodoApi.Models.DTOs;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    public static class TaskEndpoints
    {
        public static void MapTaskEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/tasks", async (ITaskService service) =>
            {
                var tasks = await service.GetUncompletedTasksAsync();
                return Results.Ok(tasks);
            })
            .WithName("GetTasks");

            app.MapPost("/api/tasks", async (TaskItemDto dto, ITaskService service) =>
            {
                if (string.IsNullOrWhiteSpace(dto.Title))
                    return Results.BadRequest("Task title is required.");

                var task = await service.CreateTaskAsync(dto);
                return Results.Created($"/api/tasks/{task.Id}", task);
            })
            .WithName("CreateTask");

            app.MapPatch("/api/tasks/{id}/done", async (int id, ITaskService service) =>
            {
                var result = await service.MarkTaskDoneAsync(id);
                return result ? Results.NoContent() : Results.NotFound();
            })
            .WithName("MarkTaskDone");
        }
    }
}
