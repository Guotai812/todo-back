using TodoApp.Tasks.Queries;

namespace TodoApp.Tasks.Services;

public class TaskService : ITaskService
{
    private readonly GetTaskByIdQueryHandler _getTaskById;
    private readonly GetAllTasksQueryHandler _getAllTasks;

    public TaskService(
        GetTaskByIdQueryHandler getTaskByIdQueryHandler,
        GetAllTasksQueryHandler getAllTasksQueryHandler)
    {
        _getTaskById = getTaskByIdQueryHandler;
        _getAllTasks = getAllTasksQueryHandler;
    }

    public Task<TaskItemDTO?> GetByIdAsync(string id)
        => _getTaskById.Handle(new GetTaskByIdQuery { id = id });

    public Task<List<TaskListDTO>> GetAllAsync()
        => _getAllTasks.Handle(new GetAllTasksQuery());
}