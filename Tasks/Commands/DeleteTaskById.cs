using System.ComponentModel.DataAnnotations;
using TodoApp.Data;

namespace TodoApp.Tasks.Commands;

public class DeleteTaskByIdCommand
{
    [Required]
    public string id { get; set; }
}

public class DeleteTaskByIdCommandHandler(ApplicationDbContext context, ILogger<DeleteTaskByIdCommandHandler> logger)
{
    public async Task<bool> Handle(DeleteTaskByIdCommand command, CancellationToken ct = default)
    {
        logger.LogInformation("Delete task: {id}", command.id);
        var task = await context.TaskItems.FindAsync(command.id);
        if (task == null)
        {
            return false;
        }
        context.TaskItems.Remove(task);
        await context.SaveChangesAsync(ct);
        return true;
    }
}