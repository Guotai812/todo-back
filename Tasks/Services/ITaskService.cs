using TodoApp.Tasks.Queries;

namespace TodoApp.Tasks.Services;

public interface ITaskService
{
    Task<TaskItemDTO?> GetByIdAsync(string id);
    Task<List<TaskListDTO>> GetAllAsync();
}