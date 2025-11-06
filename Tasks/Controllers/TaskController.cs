using Microsoft.AspNetCore.Mvc;
using TodoApp.Tasks.Commands;
using TodoApp.Tasks.Queries;
using TodoApp.Tasks.Services;

namespace TodoApp.Tasks.Controllers;
[ApiController]
[Route("/api/tasks")]
public class TaskController(ITaskService taskService ): ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTaskById(string id)
    {
        var query = new GetTaskByIdQuery
        {
            id = id
        };
        var result = await taskService.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTasks()
    {
        var result = await taskService.GetAllAsync();
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateTask(CreateTaskCommand command)
    {
        var taskId = await taskService.CreateTaskAsync(command);
        return Ok(taskId);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(string id)
    {
        var deleted = await taskService.DeleteTaskByIdAsync(id);
        return deleted ? NoContent() : NotFound();
    }
    
    [HttpPatch("{id}")]
    public async Task<IActionResult> DeleteTask(string id, UpdateTaskByIdCommand command)
    {
        var updated = await taskService.UpdateTaskByIdAsync(id,command);
        return updated ? NoContent() : NotFound();
    }
}
