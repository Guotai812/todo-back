using System.Text.Json.Serialization;

namespace TodoApp.Models;

public class Category
{
    public string id { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public string color { get; set; }
    [JsonIgnore]
    public ICollection<TaskItem> TaskItems { get; set; }
}