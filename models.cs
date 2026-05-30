
public enum TaskStatus
{
    Completed,
    Opened
}

public enum BugSeverity
{
    Low,
    Medium,
    High
}

public enum FeaturePriority
{
    Low,
    Normal,
    Urgent
}


public enum TaskType
{
    Task,
    Bug,
    Feature
}

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public TaskStatus Status { get; set; } = TaskStatus.Opened;

    public TaskType Type { get; protected set; } = TaskType.Task;

    public string AssignedTo {get; set;} = string.Empty;


}


public class BugTask : TaskItem
{
    public BugSeverity Severity {get;set;} = BugSeverity.Low;

    public BugTask()
    {
        Type = TaskType.Bug;
    }

}


public class FeatureTask : TaskItem
{
    public FeaturePriority Priority  {get;set;} = FeaturePriority.Low;

    public FeatureTask()
    {
        Type = TaskType.Feature;
    }

}