using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using TodoApp.Data;
using TodoApp.Models;

namespace TodoApp.Tasks.Commands;

public class CreateTaskCommand
{
    [Required]
    public string title { get; set; }
    public string description { get; set; }
    [Required]
    public string categoryId { get; set; }
    
}

public class CreateTaskCommandHandler(ApplicationDbContext context, ILogger<CreateTaskCommandHandler>  logger)
{
    public async Task<string> Handle(CreateTaskCommand command, CancellationToken ct = default)
    {
        logger.LogInformation("Creating new task: {Title}", command.title);

        var categoryExists = await context.Categories
            .AnyAsync(c => c.id == command.categoryId, ct);
        if (!categoryExists)
            throw new ArgumentException("Category not found");

        var task = new TaskItem
        {
            id = Guid.NewGuid().ToString(),
            title = command.title,
            description = command.description,
            categoryId = command.categoryId,
        };

        context.TaskItems.Add(task);
        await context.SaveChangesAsync(ct);

        logger.LogInformation("Created task {TaskId}", task.id);
        return task.id;
    }
}