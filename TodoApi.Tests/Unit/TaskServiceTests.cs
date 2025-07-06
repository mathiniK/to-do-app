using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Data;
using TodoApi.Models;
using TodoApi.Models.DTOs;
using TodoApi.Services;
using Xunit;

namespace TodoApi.Tests;

public class TaskServiceTests
{
    private TodoContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<TodoContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new TodoContext(options);
    }

    [Fact]
    public async Task CreateTaskAsync_SavesValidTask()
    {
        // Arrange
        var context = GetDbContext();
        var service = new TaskService(context);
        var dto = new TaskItemDto { Title = "New Task", Description = "Details" };

        // Act
        var result = await service.CreateTaskAsync(dto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("New Task", result.Title);
        Assert.False(result.IsCompleted);
        Assert.Equal("Details", result.Description);
    }

    [Fact]
    public async Task GetUncompletedTasksAsync_OnlyReturnsIncomplete()
    {
        // Arrange
        var context = GetDbContext();
        context.Tasks.AddRange(
            new TaskItem { Title = "Task A", IsCompleted = false, CreatedAt = DateTime.UtcNow },
            new TaskItem { Title = "Task B", IsCompleted = true, CreatedAt = DateTime.UtcNow }
        );
        await context.SaveChangesAsync();

        var service = new TaskService(context);

        // Act
        var result = await service.GetUncompletedTasksAsync();

        // Assert
        Assert.Single(result);
        Assert.False(result[0].IsCompleted);
    }

    [Fact]
    public async Task MarkTaskDoneAsync_SetsIsCompleted()
    {
        // Arrange
        var context = GetDbContext();
        var task = new TaskItem { Title = "Pending", IsCompleted = false, CreatedAt = DateTime.UtcNow };
        context.Tasks.Add(task);
        await context.SaveChangesAsync();

        var service = new TaskService(context);

        // Act
        var result = await service.MarkTaskDoneAsync(task.Id);

        // Assert
        Assert.True(result);
        var updated = context.Tasks.First();
        Assert.True(updated.IsCompleted);
    }

    [Fact]
    public async Task MarkTaskDoneAsync_InvalidId_ReturnsFalse()
    {
        var context = GetDbContext();
        var service = new TaskService(context);

        var result = await service.MarkTaskDoneAsync(999);

        Assert.False(result);
    }
}
