public class FeatureTaskCreator : TaskCreator
{

    public override TaskItem CreateTask(TaskCreationData data)
    {
        return new FeatureTask
        {
            Title = data.Title,
            Description = data.Description,
            Priority = data.Priority ?? FeaturePriority.Low,
            AssignedTo = data.AssignedTo
        };
    }
    
}