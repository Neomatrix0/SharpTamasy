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


    public void AddFeatureTask(string title,string description, FeaturePriority featurePriority)
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


    public void DeleteTask(int id)
    {
         using var db = new AppDbContext();
         var task = db.Tasks.FirstOrDefault(t => t.Id == id);
         if(task == null)
        {
            return;
        }
        db.Tasks.Remove(task);
         db.SaveChanges();
    }


    public TaskItem? GetTaskById(int id)
    {

         using var db = new AppDbContext();
         var task = db.Tasks.Find(id);
         return task;
            
    }

    public List<TaskItem> GetAllTasks()
    {
         using var db = new AppDbContext();
         return db.Tasks.ToList();
         
        
    }


    public void UpdateTask(int id,string newTitle, string newDescription, TaskStatus newStatus)
    {

        using var db = new AppDbContext();
          var task = db.Tasks.FirstOrDefault(t => t.Id == id);
         if(task == null)
        {
            return;
        }
        task.Title = newTitle;
        task.Description = newDescription;
        task.Status  = newStatus;
        db.SaveChanges();

       

    }


     public void UpdateFeaturePriority(int id,FeaturePriority newPriority)
    {

        using var db = new AppDbContext();
          var task = db.Tasks.FirstOrDefault(t => t.Id == id);
         if(task is not FeatureTask feature)
        {
            return;
        }
      
        feature.Priority = newPriority;
        
        db.SaveChanges();    

    }


    public void UpdateBugSeverity(int id, BugSeverity newSeverity)
    {
        using var db = new AppDbContext();
        var task =  db.Tasks.FirstOrDefault(t => t.Id == id);

         if (task is not BugTask bug)
    {
        return;
    }
        
        bug.Severity = newSeverity;
        
        db.SaveChanges();
    }
}


