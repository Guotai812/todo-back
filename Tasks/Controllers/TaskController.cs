using Microsoft.AspNetCore.Mvc;
using TodoApp.Tasks.Commands;
using TodoApp.Tasks.Queries;

namespace TodoApp.Tasks.Controllers;
[ApiController]
[Route("/api/tasks")]
public class TaskController(
    UpdateTaskByIdCommandHandler updateTaskByIdCommandHandler, 
    DeleteTaskByIdCommandHandler deleteTaskByIdCommandHandler, 
    CreateTaskCommandHandler createTaskCommandHandler, 
    GetAllTasksQueryHandler getAllTasksQueryHandler, 
    GetTaskByIdQueryHandler getTaskByIdQueryHandler ): ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTaskById(string id)
    {
        var result = await getTaskByIdQueryHandler.Handle(new GetTaskByIdQuery { id = id });
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTasks()
    {
        var result = await getAllTasksQueryHandler.Handle(new  GetAllTasksQuery());
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateTask(CreateTaskCommand command)
    {
        var taskId = await createTaskCommandHandler.Handle(command);
        return Ok(taskId);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(string id)
    {
        var deleted = await deleteTaskByIdCommandHandler.Handle(new DeleteTaskByIdCommand { id = id });
        return deleted ? NoContent() : NotFound();
    }
    
    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateTask(string id, UpdateTaskByIdCommand command)
    {
        var updated = await updateTaskByIdCommandHandler.Handle(id, command);
        return updated ? NoContent() : NotFound();
    }
}
