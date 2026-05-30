public class BasicTaskCreator : TaskCreator
{
    public override TaskItem CreateTask(TaskCreationData data)
    {
       return new TaskItem
       {
        Title = data.Title,
        Description = data.Description,
        AssignedTo = data.AssignedTo
           
       };
    
        
    }

}