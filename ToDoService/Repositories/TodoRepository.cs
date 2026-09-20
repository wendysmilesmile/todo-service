using ToDoService.Models;

namespace ToDoService.Repositories;

public class TodoRepository : ITodoRepository
{
    // In-memory List used as demo storage.
    private readonly List<TodoItem> _items = [];

    // Auto-incrementing id seed for new items.
    private int _nextId = 1;

    // Returns only items that are not soft-deleted.
    public TodoItem[] List()
    {
        return _items
            .Where(x => !x.IsDeleted)
            .ToArray();
    }

    // Queries one active item by id.
    public TodoItem? Query(int id)
    {
        return _items
            .FirstOrDefault(x => x.Id == id && !x.IsDeleted);
    }

    // Adds a new item into the in-memory array.
    public TodoItem Add(string title)
    {
        var newItem = new TodoItem
        {
            Id = _nextId++,
            Title = title,
            IsDeleted = false
        };

        _items.Add(newItem);

        return newItem;
    }

    // Edits an existing active item title.
    public TodoItem? Edit(int id, string title)
    {
        var item = _items.First(x => x.Id == id);

        item.Title = title;
        return item;
    }

    // Soft-deletes an item by setting IsDeleted = true.
    public bool Delete(int id)
    {
        var item = _items.First(x => x.Id == id);

        item.IsDeleted = true;
        return true;
    }
}
