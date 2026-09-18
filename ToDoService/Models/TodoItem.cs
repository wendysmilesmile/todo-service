namespace ToDoService.Models;

public class TodoItem
{
    // Unique identifier of the todo item.
    public int Id { get; set; }

    // User-provided title or content of the todo item.
    public string Title { get; set; } = string.Empty;

    // Soft-delete flag. True means logically deleted.
    public bool IsDeleted { get; set; }
}
