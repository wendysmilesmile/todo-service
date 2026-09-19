namespace ToDoService.Models;

// Request body for editing a todo item.
public class EditTodoRequest
{
    public string Title { get; set; } = string.Empty;
}
