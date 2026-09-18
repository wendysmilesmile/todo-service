using ToDoService.Models;

namespace ToDoService.Repositories;

// Repository contract for todo persistence.
public interface ITodoRepository
{
    // Returns all active todo items.
    TodoItem[] List();

    // Creates and stores a new todo item.
    TodoItem Add(string title);

    // Soft-deletes a todo item by id.
    bool Delete(int id);
}
