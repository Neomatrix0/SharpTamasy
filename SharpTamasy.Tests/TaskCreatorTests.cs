namespace SharpTamasy.Tests;

public class TaskCreatorTests
{
    [Fact]
    public void BugTaskCreator_WithSeverity_ReturnsBugTaskWithCorrectData()
    {
        var creator = new BugTaskCreator();
        var data = new TaskCreationData
        {
            Title = "Bug login",
            Description = "Errore login",
            Severity = BugSeverity.High
        };

        var task = creator.CreateTask(data);

        var bugTask = Assert.IsType<BugTask>(task);
        Assert.Equal("Bug login", bugTask.Title);
        Assert.Equal("Errore login", bugTask.Description);
        Assert.Equal(BugSeverity.High, bugTask.Severity);
        Assert.Equal(TaskType.Bug, bugTask.Type);
    }

    [Fact]
    public void BugTaskCreator_WithoutSeverity_UsesLowAsDefault()
    {
        var creator = new BugTaskCreator();
        var data = new TaskCreationData
        {
            Title = "Bug generico",
            Description = "Errore"
        };

        var task = creator.CreateTask(data);

        var bugTask = Assert.IsType<BugTask>(task);
        Assert.Equal(BugSeverity.Low, bugTask.Severity);
        Assert.Equal(TaskType.Bug, bugTask.Type);
    }

    [Fact]
    public void FeatureTaskCreator_WithPriority_ReturnsFeatureTaskWithCorrectData()
    {
        var creator = new FeatureTaskCreator();
        var data = new TaskCreationData
        {
            Title = "New Feature",
            Description = "implement new operation",
            Priority = FeaturePriority.Normal
        };

        var task = creator.CreateTask(data);

        var featureTask = Assert.IsType<FeatureTask>(task);
        Assert.Equal("New Feature", featureTask.Title);
        Assert.Equal("implement new operation", featureTask.Description);
        Assert.Equal(FeaturePriority.Normal, featureTask.Priority);
        Assert.Equal(TaskType.Feature, featureTask.Type);
    }

    [Fact]
    public void FeatureTaskCreator_WithoutPriority_UsesLowAsDefault()
    {
        var creator = new FeatureTaskCreator();
        var data = new TaskCreationData
        {
            Title = "Feature generica",
            Description = "Nuova funzionalita"
        };

        var task = creator.CreateTask(data);

        var featureTask = Assert.IsType<FeatureTask>(task);
        Assert.Equal(FeaturePriority.Low, featureTask.Priority);
        Assert.Equal(TaskType.Feature, featureTask.Type);
    }

    [Fact]
    public void BasicTaskCreator_CreateTask_ReturnsTaskItemWithCorrectData()
    {
        var creator = new BasicTaskCreator();
        var data = new TaskCreationData
        {
            Title = "New Basic task",
            Description = "Normal operation"
        };

        var task = creator.CreateTask(data);

        Assert.IsType<TaskItem>(task);
        Assert.Equal("New Basic task", task.Title);
        Assert.Equal("Normal operation", task.Description);
        Assert.Equal(TaskType.Task, task.Type);
    }

    [Fact]
    public void BugTask_ShouldStoreSeverityAndType()
    {
        var bugTask = new BugTask
        {
            Title = "Fix security risk",
            Description = "Urgent operation",
            Severity = BugSeverity.High
        };

        Assert.Equal(BugSeverity.High, bugTask.Severity);
        Assert.Equal(TaskType.Bug, bugTask.Type);
    }

    [Fact]
    public void FeatureTask_ShouldStorePriorityAndType()
    {
        var featureTask = new FeatureTask
        {
            Title = "Export PDF",
            Description = "Aggiungere export PDF",
            Priority = FeaturePriority.Urgent
        };

        Assert.Equal(FeaturePriority.Urgent, featureTask.Priority);
        Assert.Equal(TaskType.Feature, featureTask.Type);
    }
}
