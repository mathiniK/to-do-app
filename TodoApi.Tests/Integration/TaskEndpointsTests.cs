using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TodoApi.Models.DTOs;
using Xunit;

namespace TodoApi.Tests.Integration;

public class TaskEndpointsTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public TaskEndpointsTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task POST_CreateTask_Valid_ReturnsCreated()
    {
        var dto = new TaskItemDto { Title = "Integration Task", Description = "Test desc" };

        var response = await _client.PostAsJsonAsync("/api/tasks", dto);

        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task POST_CreateTask_Invalid_ReturnsBadRequest()
    {
        var dto = new TaskItemDto { Title = "" };

        var response = await _client.PostAsJsonAsync("/api/tasks", dto);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GET_UncompletedTasks_ReturnsOk()
    {
        var response = await _client.GetAsync("/api/tasks");

        response.EnsureSuccessStatusCode();
        var tasks = await response.Content.ReadFromJsonAsync<List<object>>();

        Assert.NotNull(tasks);
    }

    [Fact]
    public async Task PATCH_MarkTaskDone_ValidId_ReturnsNoContent()
    {
        // Arrange: Create a task
        var create = new TaskItemDto { Title = "To be completed" };
        var post = await _client.PostAsJsonAsync("/api/tasks", create);
        var task = await post.Content.ReadFromJsonAsync<TaskItemDto>();
        int id = task!.Id;

        // Act
        var patch = await _client.PatchAsync($"/api/tasks/{id}/done", null);

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, patch.StatusCode);
    }

    [Fact]
    public async Task PATCH_MarkTaskDone_InvalidId_ReturnsNotFound()
    {
        var patch = await _client.PatchAsync("/api/tasks/99999/done", null);
        Assert.Equal(HttpStatusCode.NotFound, patch.StatusCode);
    }
}
