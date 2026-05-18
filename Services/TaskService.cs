using Microsoft.EntityFrameworkCore;

public class TaskService
{
    public void AddTask(string title,string description)
    {
        using var db = new AppDbContext();
        var task = new TaskItem
        {
            Title = title,
            Description = description
        };
        db.Tasks.Add(task);
        db.SaveChanges();

    }



    public void AddBugTask(string title,string description, BugSeverity severity)
    {
        using var db = new AppDbContext();
        var bugtask = new BugTask
        {
            Title = title,
            Description = description,
            Severity = severity
        };

        db.Tasks.Add(bugtask);
        db.SaveChanges();
    }


    public void FeatureTask(string title,string description, FeaturePriority featurePriority)
    {
        using var db = new AppDbContext();
        var featuretask = new FeatureTask
        {
            Title = title,
            Description = description,
            Priority = featurePriority
        };
        db.Tasks.Add(featuretask);
        db.SaveChanges();
    }
}