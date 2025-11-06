namespace TodoApp.Models;

public class TaskItem
{
    public string id { get; set; } = Guid.NewGuid().ToString();
    public string title { get; set; }
    public string description { get; set; }
    
    public string categoryId { get; set; }
    
    public Category category { get; set; }
    
}