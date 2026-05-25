using Microsoft.EntityFrameworkCore;


public class TaskService
{


    private readonly AppDbContext _db;
    public TaskService(AppDbContext db)
    {
        _db = db;
    }
    public void AddTask(TaskItem task)
    {
        _db.Tasks.Add(task);
        _db.SaveChanges();

    }



    public void DeleteTask(int id)
    {
         var task = _db.Tasks.FirstOrDefault(t => t.Id == id);
         if(task == null)
        {
            return;
        }
        _db.Tasks.Remove(task);
         _db.SaveChanges();
    }


    public TaskItem? GetTaskById(int id)
    {

         var task = _db.Tasks.Find(id);
         return task;
            
    }

    public List<TaskItem> GetAllTasks()
    {
         return _db.Tasks.ToList();
         
        
    }


    public void UpdateTask(int id,string newTitle, string newDescription, TaskStatus newStatus)
    {

        var task = _db.Tasks.FirstOrDefault(t => t.Id == id);
         if(task == null)
        {
            return;
        }
        task.Title = newTitle;
        task.Description = newDescription;
        task.Status  = newStatus;
        _db.SaveChanges();

       

    }


     public void UpdateFeaturePriority(int id,FeaturePriority newPriority)
    {

          var task = _db.Tasks.FirstOrDefault(t => t.Id == id);
         if(task is not FeatureTask feature)
        {
            return;
        }
      
        feature.Priority = newPriority;
        
        _db.SaveChanges();    

    }


    public void UpdateBugSeverity(int id, BugSeverity newSeverity)
    {
        var task = _db.Tasks.FirstOrDefault(t => t.Id == id);

         if (task is not BugTask bug)
    {
        return;
    }
        
        bug.Severity = newSeverity;
        
        _db.SaveChanges();
    }
}


