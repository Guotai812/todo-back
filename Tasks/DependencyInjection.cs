using Microsoft.Extensions.DependencyInjection;
using TodoApp.Tasks.Queries;
using TodoApp.Tasks.Services;

namespace TodoApp.Tasks;

public static class DependencyInjection
{
    public static IServiceCollection AddTaskServices(this IServiceCollection services)
    {
        services.AddScoped<GetTaskByIdQueryHandler>();
        services.AddScoped<GetAllTasksQueryHandler>();

        services.AddScoped<ITaskService, TaskService>();


        return services;
    }
}