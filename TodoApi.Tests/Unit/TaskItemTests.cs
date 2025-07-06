using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;
using Xunit;

namespace TodoApi.Tests;

public class TaskItemTests
{
    private TodoContext GetInMemoryDb()
    {
        var options = new DbContextOptionsBuilder<TodoContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // unique DB per test
            .Options;

        return new TodoContext(options);
    }

    [Fact]
    public async Task Can_Create_And_Retrieve_Task()
    {
        using var db = GetInMemoryDb();

        var task = new TaskItem
        {
            Title = "Test Task",
            Description = "Test Desc",
            CreatedAt = DateTime.UtcNow
        };

        db.Tasks.Add(task);
        await db.SaveChangesAsync();

        var saved = await db.Tasks.FirstOrDefaultAsync(t => t.Title == "Test Task");

        Assert.NotNull(saved);
        Assert.Equal("Test Desc", saved.Description);
    }
}
