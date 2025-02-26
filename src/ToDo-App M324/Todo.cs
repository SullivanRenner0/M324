namespace ToDo_App_M324.Logic;

public class Todo
{
    public long Id { get; set; }
    public string Header { get; set; } = "";
    public string Description { get; set; } = "";
    public TodoStatus Status { get; set; }
    public TodoPriority Priority { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? Deadline { get; set; }
}
