using Microsoft.EntityFrameworkCore;
using Xunit;
namespace SharpTamasy.Tests;

public class TaskServiceTests
{
    private AppDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public void AddTask_ShouldSaveTask()
    {
        using var db = CreateDbContext();
        var service = new TaskService(db);

        var task = new TaskItem
        {
            Title = "Studiare C#",
            Description = "Unit test"
        };

        service.AddTask(task);

        Assert.Single(db.Tasks);
        Assert.Equal("Studiare C#", db.Tasks.First().Title);
        Assert.Equal("Unit test", db.Tasks.First().Description);
    }

     [Fact]
    public void DeleteTask_WithExistingId_ShouldRemoveTask()
    {
        using var db = CreateDbContext();
        var service = new TaskService(db);

        var task = new TaskItem
    {
        Title = "Task da eliminare",
        Description = "Test delete"
    };

    db.Tasks.Add(task);
    db.SaveChanges();
    service.DeleteTask(task.Id);
     Assert.Empty(db.Tasks);
    
    }
}