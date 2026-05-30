public class TaskCreationData{
    public string Title {get;set;} = string.Empty;
    public string Description {get;set;} = string.Empty;
    public BugSeverity? Severity{get;set;}

    public FeaturePriority? Priority {get;set;}

    public string AssignedTo {get;set;} = string.Empty;
    
}
