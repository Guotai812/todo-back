using Microsoft.EntityFrameworkCore;
using TodoApp.Data;

namespace TodoApp.Tasks.Commands;

public class UpdateTaskByIdCommand
{
    public string title { get; set; }
    public string description { get; set; }
    public string categoryId { get; set; }
}

public class UpdateTaskByIdCommandHandler(ApplicationDbContext context, ILogger<UpdateTaskByIdCommandHandler> logger)
{
    public async Task<bool> Handle(string id, UpdateTaskByIdCommand command, CancellationToken ct = default)
    {
        logger.LogInformation("update task: {Id}", id);
        var task = await context.TaskItems
            .SingleOrDefaultAsync(t => t.id == id, ct);
        if (task == null)
        {
            return false;
        }
        
        if (command.title != null)
            task.title = command.title;

        if (command.description != null)
            task.description = command.description;
        
        if (command.categoryId != null)
            task.categoryId = command.categoryId;

        await context.SaveChangesAsync();
        logger.LogInformation("update task: {Id} successfully", id);
        return true;
    }
} 