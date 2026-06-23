using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/tasks")]
public class TasksController : ControllerBase
{
    [HttpGet]
    public IActionResult GetTasks()
    {
        var tasks = new[]
        {
            new
            {
                Id = 1,
                Title = "First task",
                Description = "Test from backend",
                Type = "Task",
                Status = "Opened",
                AssignedTo = "Daniele"
            }
        };

        return Ok(tasks);
    }
}