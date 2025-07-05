public class TaskControllerTests
{
    private TaskItemsContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<TaskItemsContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        return new TaskItemsContext(options);
    }

    [Fact]
    public async Task GetTasks_ReturnsList()
    {
        // Arrange
        var context = GetDbContext();
        context.TaskItems.Add(new TaskItem { Title = "Sample", Description = "Desc" });
        context.SaveChanges();

        var controller = new TaskItemsController(context);

        // Act
        var result = await controller.GetTaskItems();

        // Assert
        Assert.Single(result.Value);
    }
}
