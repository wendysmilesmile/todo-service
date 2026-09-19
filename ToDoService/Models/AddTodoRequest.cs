namespace ToDoService.Models;

// Request body for creating a todo item.
public class AddTodoRequest
{
    public string Title { get; set; } = string.Empty;
}
