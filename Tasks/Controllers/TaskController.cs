using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Tasks.Queries;
using TodoApp.Tasks.Services;

namespace TodoApp.Tasks.Controllers;
[ApiController]
[Route("/api/tasks/")]
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
}