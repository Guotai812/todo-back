using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using TodoApp.Data;

namespace TodoApp.Tasks.Queries;

public class GetTaskByIdQuery
{
    [Required]
    public string id { get; set; }
}

public class GetTaskByIdQueryHandler(ApplicationDbContext context, ILogger<GetTaskByIdQueryHandler> logger)
{
    public async Task<TaskItemDTO> Handle(GetTaskByIdQuery query)
    {
        logger.LogInformation("Fetch task: {id}",  query.id);
        var task = await context.TaskItems
            .Include(t => t.category)
            .FirstOrDefaultAsync(t => t.id == query.id);
        logger.LogInformation("Fetch task: {id}", query.id);
        return new TaskItemDTO
        {
            title = task.title,
            description = task.description,
            categoryName = task.category.name,
        };
    }
}

public class TaskItemDTO
{
    public string title { get; set; }
    public string description { get; set; }
    public string categoryName { get; set; }
}

