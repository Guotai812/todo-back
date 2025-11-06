using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Tasks.Commands;
using TodoApp.Tasks.Queries;

namespace TodoApp.Tasks.Services;

public class TaskService : ITaskService
{
    private readonly GetTaskByIdQueryHandler _getTaskById;
    private readonly GetAllTasksQueryHandler _getAllTasks;
    private readonly CreateTaskCommandHandler _createTask;
    private readonly DeleteTaskByIdCommandHandler _deleteTaskById;
    private readonly UpdateTaskByIdCommandHandler _updateTaskById;

    public TaskService(
        GetTaskByIdQueryHandler getTaskByIdQueryHandler,
        GetAllTasksQueryHandler getAllTasksQueryHandler,
        CreateTaskCommandHandler createTask,
        DeleteTaskByIdCommandHandler deleteTaskById,
        UpdateTaskByIdCommandHandler updateTaskById)
    {
        _getTaskById = getTaskByIdQueryHandler;
        _getAllTasks = getAllTasksQueryHandler;
        _createTask = createTask;
        _deleteTaskById = deleteTaskById;
        _updateTaskById = updateTaskById;
    }

    public Task<TaskItemDTO?> GetByIdAsync(string id)
    {
        return _getTaskById.Handle(new GetTaskByIdQuery { id = id });
    }

    public Task<List<TaskListDTO>> GetAllAsync()
    {
        return _getAllTasks.Handle(new GetAllTasksQuery());
    }
    public Task<String> CreateTaskAsync(CreateTaskCommand command)
    {
        return _createTask.Handle(command);
    }

    public Task<bool> DeleteTaskByIdAsync(string id)
    {
        return _deleteTaskById.Handle(new DeleteTaskByIdCommand{id = id});
    }

    public Task<bool> UpdateTaskByIdAsync(string id, UpdateTaskByIdCommand command)
    {
        return _updateTaskById.Handle(id,command);
    }
}