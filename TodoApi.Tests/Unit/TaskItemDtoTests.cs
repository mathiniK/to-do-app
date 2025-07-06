using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TodoApi.Models.DTOs;
using Xunit;

namespace TodoApi.Tests;

public class TaskItemDtoTests
{
    [Fact]
    public void TaskItemDto_ValidTitle_PassesValidation()
    {
        var dto = new TaskItemDto { Title = "Test Task" };

        var context = new ValidationContext(dto);
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(dto, context, results, true);

        Assert.True(isValid);
    }

    [Fact]
    public void TaskItemDto_EmptyTitle_FailsValidation()
    {
        var dto = new TaskItemDto { Title = "" };

        var context = new ValidationContext(dto);
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(dto, context, results, true);

        Assert.False(isValid);
        Assert.Contains(results, r => r.ErrorMessage == "Title is required.");
    }
}
