using Microsoft.EntityFrameworkCore;
using TodoApp.Data;
using TodoApp.Models;

namespace TodoApp.Tasks.Queries;

public class GetAllTasksQuery {}

public class GetAllTasksQueryHandler(ApplicationDbContext context, ILogger<GetAllTasksQueryHandler> logger)
{
    public async Task<List<TaskListDTO>> Handle(GetAllTasksQuery query,CancellationToken ct = default)
    {
        logger.LogInformation("Fetch all tasks");
        List<TaskListDTO> taskListDtos = await context.TaskItems
            .Select(t => new TaskListDTO
            {
                id = t.id,
                title = t.title,
                description = t.description,
                categoryName  = t.category != null ? t.category.name  : null,
                categoryColor = t.category != null ? t.category.color : null
            })
            .ToListAsync(ct);
        logger.LogInformation("Fetch all tasks");
        return taskListDtos;
    }
}

public class TaskListDTO
{
    public string id { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public string categoryName { get; set; }
    public string categoryColor { get; set; }
}