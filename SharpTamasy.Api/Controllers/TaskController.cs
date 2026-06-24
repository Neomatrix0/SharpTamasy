using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharpTamasy.Api.Contracts;

namespace SharpTamasy.Api.Controllers;

[ApiController]
[Route("api/tasks")]
public class TasksController : ControllerBase
{
    private readonly AppDbContext _db;

    public TasksController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetTasks()
    {
        var taskEntities = await _db.Tasks
            .AsNoTracking()
            .OrderByDescending(t => t.Id)
            .ToListAsync();

        return Ok(taskEntities.Select(ToResponse));
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskRequest data)
    {
        TaskItem task = data.Type switch
        {
            TaskType.Bug => new BugTask
            {
                Severity = data.Severity ?? BugSeverity.Low
            },
            TaskType.Feature => new FeatureTask
            {
                Priority = data.Priority ?? FeaturePriority.Low
            },
            _ => new TaskItem()
        };

        task.Title = data.Title.Trim();
        task.Description = data.Description.Trim();
        task.AssignedTo = data.AssignedTo.Trim();

        _db.Tasks.Add(task);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTasks), new { id = task.Id }, ToResponse(task));
    }

    private static TaskResponse ToResponse(TaskItem task) => new(
        task.Id,
        task.Title,
        task.Description,
        task.Type.ToString(),
        task.Status.ToString(),
        task.AssignedTo,
        task is BugTask bug ? bug.Severity.ToString() : null,
        task is FeatureTask feature ? feature.Priority.ToString() : null);
}
