using TodoApp.Tasks.Commands;
using TodoApp.Tasks.Queries;

namespace TodoApp.Tasks.Services;

public interface ITaskService
{
    Task<TaskItemDTO?> GetByIdAsync(string id);
    Task<List<TaskListDTO>> GetAllAsync();
    Task<String> CreateTaskAsync(CreateTaskCommand command);
    Task<bool> DeleteTaskByIdAsync(string id);
    Task<bool> UpdateTaskByIdAsync(string id, UpdateTaskByIdCommand command);
}