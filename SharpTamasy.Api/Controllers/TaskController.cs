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
        var tasks = await _db.Tasks
            .AsNoTracking()
            .OrderByDescending(t => t.Id)
            .Select(t => new
            {
                t.Id,
                t.Title,
                t.Description,
                Type = t.Type.ToString(),
                Status = t.Status.ToString(),
                t.AssignedTo
            })
            .ToListAsync();

        return Ok(tasks);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskRequest data)
    {
        var task = new TaskItem
        {
            Title = data.Title,
            Description = data.Description,
            AssignedTo = data.AssignedTo
        };

        _db.Tasks.Add(task);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTasks), new { id = task.Id }, new
        {
            task.Id,
            task.Title,
            task.Description,
            Type = task.Type.ToString(),
            Status = task.Status.ToString(),
            task.AssignedTo
        });
    }
}
