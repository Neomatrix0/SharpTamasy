using Microsoft.EntityFrameworkCore;


public class TaskService
{
    public void AddTask(TaskItem task)
    {
        using var db = new AppDbContext();
       
        db.Tasks.Add(task);
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


