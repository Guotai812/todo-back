namespace TodoApp.Models;

public class TaskItem
{
    public string id { get; set; } = Guid.NewGuid().ToString();
    public string title { get; set; }
    public string description { get; set; }
    
}