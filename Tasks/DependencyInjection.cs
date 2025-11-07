using Microsoft.Extensions.DependencyInjection;
using TodoApp.Tasks.Commands;
using TodoApp.Tasks.Queries;

namespace TodoApp.Tasks;

public static class DependencyInjection
{
    public static IServiceCollection AddTaskServices(this IServiceCollection services)
    {
        services.AddScoped<GetTaskByIdQueryHandler>();
        services.AddScoped<GetAllTasksQueryHandler>();
        services.AddScoped<CreateTaskCommandHandler>();
        services.AddScoped<DeleteTaskByIdCommandHandler>();
        services.AddScoped<UpdateTaskByIdCommandHandler>();

        
        return services;
    }
}