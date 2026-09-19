using ToDoService.Models;

namespace ToDoService.Repositories;

// Repository contract for todo persistence.
public interface ITodoRepository
{
    // Returns all active todo items.
    TodoItem[] List();

    // Queries one active todo item by id.
    TodoItem? Query(int id);

    // Creates and stores a new todo item.
    TodoItem Add(string title);

    // Edits an existing todo item title.
    TodoItem? Edit(int id, string title);

    // Soft-deletes a todo item by id.
    bool Delete(int id);
}
