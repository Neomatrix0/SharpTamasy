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
    public void AddTask_ShouldSaveAssignedTo()
    {
        using var db = CreateDbContext();
        var service = new TaskService(db);

        var task = new TaskItem
        {
            Title = "Preparare demo",
            Description = "Mostrare la nuova feature",
            AssignedTo = "Daniel"
        };

        service.AddTask(task);

        var savedTask = Assert.Single(db.Tasks);
        Assert.Equal("Daniel", savedTask.AssignedTo);
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

    [Fact]
    public void DeleteTask_WithMissingId_ShouldLeaveExistingTasksUntouched()
    {
        using var db = CreateDbContext();
        var service = new TaskService(db);
        var task = new TaskItem
        {
            Title = "Task da mantenere",
            Description = "Non deve essere eliminato"
        };

        db.Tasks.Add(task);
        db.SaveChanges();

        service.DeleteTask(task.Id + 1);

        Assert.Single(db.Tasks);
        Assert.Equal("Task da mantenere", db.Tasks.First().Title);
    }

    [Fact]
    public void GetTaskById_WithExistingId_ShouldReturnTask()
    {
        using var db = CreateDbContext();
        var service = new TaskService(db);
        var task = new TaskItem
        {
            Title = "Task da recuperare",
            Description = "Test get by id"
        };

        db.Tasks.Add(task);
        db.SaveChanges();

        var result = service.GetTaskById(task.Id);

        Assert.NotNull(result);
        Assert.Equal(task.Id, result.Id);
        Assert.Equal("Task da recuperare", result.Title);
    }

    [Fact]
    public void GetTaskById_WithMissingId_ShouldReturnNull()
    {
        using var db = CreateDbContext();
        var service = new TaskService(db);

        var result = service.GetTaskById(999);

        Assert.Null(result);
    }

    [Fact]
    public void GetAllTasks_ShouldReturnAllSavedTasks()
    {
        using var db = CreateDbContext();
        var service = new TaskService(db);
        db.Tasks.AddRange(
            new TaskItem { Title = "Task base", Description = "Primo task" },
            new BugTask { Title = "Bug critico", Description = "Secondo task", Severity = BugSeverity.High },
            new FeatureTask { Title = "Nuova feature", Description = "Terzo task", Priority = FeaturePriority.Urgent });
        db.SaveChanges();

        var tasks = service.GetAllTasks();

        Assert.Equal(3, tasks.Count);
        Assert.Contains(tasks, task => task.Title == "Task base");
        Assert.Contains(tasks, task => task is BugTask bug && bug.Severity == BugSeverity.High);
        Assert.Contains(tasks, task => task is FeatureTask feature && feature.Priority == FeaturePriority.Urgent);
    }

    [Fact]
    public void UpdateTask_WithExistingId_ShouldUpdateTitleDescriptionAndStatus()
    {
        using var db = CreateDbContext();
        var service = new TaskService(db);
        var task = new TaskItem
        {
            Title = "Titolo vecchio",
            Description = "Descrizione vecchia",
            Status = TaskStatus.Opened
        };
        db.Tasks.Add(task);
        db.SaveChanges();

        service.UpdateTask(task.Id, "Titolo nuovo", "Descrizione nuova", TaskStatus.Completed);

        var updatedTask = Assert.Single(db.Tasks);
        Assert.Equal("Titolo nuovo", updatedTask.Title);
        Assert.Equal("Descrizione nuova", updatedTask.Description);
        Assert.Equal(TaskStatus.Completed, updatedTask.Status);
    }

    [Fact]
    public void UpdateTask_WithMissingId_ShouldLeaveExistingTaskUntouched()
    {
        using var db = CreateDbContext();
        var service = new TaskService(db);
        var task = new TaskItem
        {
            Title = "Titolo originale",
            Description = "Descrizione originale",
            Status = TaskStatus.Opened
        };
        db.Tasks.Add(task);
        db.SaveChanges();

        service.UpdateTask(task.Id + 1, "Titolo nuovo", "Descrizione nuova", TaskStatus.Completed);

        var unchangedTask = Assert.Single(db.Tasks);
        Assert.Equal("Titolo originale", unchangedTask.Title);
        Assert.Equal("Descrizione originale", unchangedTask.Description);
        Assert.Equal(TaskStatus.Opened, unchangedTask.Status);
    }

    [Fact]
    public void UpdateAssignedTo_WithExistingId_ShouldUpdateAssignedTo()
    {
        using var db = CreateDbContext();
        var service = new TaskService(db);
        var task = new TaskItem
        {
            Title = "Task assegnata",
            Description = "Cambio assegnatario",
            AssignedTo = "Daniel"
        };
        db.Tasks.Add(task);
        db.SaveChanges();

        service.UpdateAssignedTo(task.Id, "Sara");

        var updatedTask = Assert.Single(db.Tasks);
        Assert.Equal("Sara", updatedTask.AssignedTo);
    }

    [Fact]
    public void UpdateAssignedTo_WithMissingId_ShouldLeaveExistingTaskUntouched()
    {
        using var db = CreateDbContext();
        var service = new TaskService(db);
        var task = new TaskItem
        {
            Title = "Task assegnata",
            Description = "Non deve cambiare",
            AssignedTo = "Daniel"
        };
        db.Tasks.Add(task);
        db.SaveChanges();

        service.UpdateAssignedTo(task.Id + 1, "Sara");

        var unchangedTask = Assert.Single(db.Tasks);
        Assert.Equal("Daniel", unchangedTask.AssignedTo);
    }

    [Fact]
    public void UpdateFeaturePriority_WithFeatureTask_ShouldUpdatePriority()
    {
        using var db = CreateDbContext();
        var service = new TaskService(db);
        var feature = new FeatureTask
        {
            Title = "Export",
            Description = "Aggiungere export",
            Priority = FeaturePriority.Low
        };
        db.Tasks.Add(feature);
        db.SaveChanges();

        service.UpdateFeaturePriority(feature.Id, FeaturePriority.Urgent);

        var updatedFeature = Assert.IsType<FeatureTask>(db.Tasks.Single());
        Assert.Equal(FeaturePriority.Urgent, updatedFeature.Priority);
    }

    [Fact]
    public void UpdateFeaturePriority_WithBasicTask_ShouldDoNothing()
    {
        using var db = CreateDbContext();
        var service = new TaskService(db);
        var task = new TaskItem
        {
            Title = "Task normale",
            Description = "Non e una feature"
        };
        db.Tasks.Add(task);
        db.SaveChanges();

        service.UpdateFeaturePriority(task.Id, FeaturePriority.Urgent);

        var unchangedTask = Assert.IsType<TaskItem>(db.Tasks.Single());
        Assert.Equal(TaskType.Task, unchangedTask.Type);
    }

    [Fact]
    public void UpdateBugSeverity_WithBugTask_ShouldUpdateSeverity()
    {
        using var db = CreateDbContext();
        var service = new TaskService(db);
        var bug = new BugTask
        {
            Title = "Login rotto",
            Description = "Errore in accesso",
            Severity = BugSeverity.Low
        };
        db.Tasks.Add(bug);
        db.SaveChanges();

        service.UpdateBugSeverity(bug.Id, BugSeverity.High);

        var updatedBug = Assert.IsType<BugTask>(db.Tasks.Single());
        Assert.Equal(BugSeverity.High, updatedBug.Severity);
    }

    [Fact]
    public void UpdateBugSeverity_WithFeatureTask_ShouldDoNothing()
    {
        using var db = CreateDbContext();
        var service = new TaskService(db);
        var feature = new FeatureTask
        {
            Title = "Dashboard",
            Description = "Nuova dashboard",
            Priority = FeaturePriority.Normal
        };
        db.Tasks.Add(feature);
        db.SaveChanges();

        service.UpdateBugSeverity(feature.Id, BugSeverity.High);

        var unchangedFeature = Assert.IsType<FeatureTask>(db.Tasks.Single());
        Assert.Equal(FeaturePriority.Normal, unchangedFeature.Priority);
    }
}
