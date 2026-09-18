using ToDoService.Models;

namespace ToDoService.Services;

// Service contract for todo business operations.
public interface ITodoService
{
    // Returns all active todo items.
    TodoItem[] List();

    // Creates a new todo item.
    TodoItem Add(string title);

    // Soft-deletes a todo item by id.
    bool Delete(int id);
}
