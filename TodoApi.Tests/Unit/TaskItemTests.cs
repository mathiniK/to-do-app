using Xunit;
using TodoApi.Models;

namespace TodoApi.Tests;

public class TaskItemTests
{
    [Fact]
    public void TaskItem_HasValidDefaults()
    {
        var task = new TaskItem();

        Assert.False(task.IsCompleted);      
        Assert.Null(task.Title);              
    }
}
