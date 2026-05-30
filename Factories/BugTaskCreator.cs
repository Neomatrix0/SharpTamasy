public class BugTaskCreator : TaskCreator
{
    public override TaskItem CreateTask(TaskCreationData data)
    {
       return new BugTask
       {
        Title = data.Title,
        Description = data.Description,
        Severity = data.Severity ?? BugSeverity.Low,
        AssignedTo = data.AssignedTo
           
       };
    
        
    }

}