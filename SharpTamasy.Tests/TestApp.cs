namespace SharpTamasy.Tests;


public class TaskCreatorTests
{
    [Fact]
    public void CreateTask_WithSeverity_ReturnsBugTaskWithCorrectData()
    {
        //Arrange

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


    }

    [Fact]
    public void CreateTask_WithoutSeverity_UsesLowAsDefault()
    {
        
        var creator = new BugTaskCreator();
        var data = new TaskCreationData
        {
            Title = "Bug generico",
            Description = "Errore",
          

        };

        var task = creator.CreateTask(data);
        var bugTask = Assert.IsType<BugTask>(task);
        Assert.Equal(BugSeverity.Low, bugTask.Severity);
    }

 [Fact]
    public void FeatureTaskCreator_CreateTask_ReturnsFeatureTask()
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
 
    }

  [Fact]  
   public void BasicTaskCreator_CreateTask_ReturnsTaskItem()
{
    var creator = new BasicTaskCreator();

    var data = new TaskCreationData
        {
            Title = "New Basic task",
            Description = "Normal operation",
           

        };

        var task = creator.CreateTask(data);
        Assert.IsType<TaskItem>(task);
        Assert.Equal("New Basic task", task.Title);
        Assert.Equal("Normal operation", task.Description);
      

    }

    [Fact]
public void BugTask_ShouldStoreSeverity()
    {
  ;

    var bugTask = new BugTask
        {
            Title = "Fix security risk",
            Description = "Urgent operation",
            Severity = BugSeverity.High
           

        };

     
        Assert.Equal(BugSeverity.High,bugTask.Severity);
    
      

    }

[Fact]
public void FeatureTask_ShouldStorePriority()
{
    var featureTask = new FeatureTask
    {
        Title = "Export PDF",
        Description = "Aggiungere export PDF",
        Priority = FeaturePriority.Urgent
    };

    Assert.Equal(FeaturePriority.Urgent, featureTask.Priority);
}

    
}