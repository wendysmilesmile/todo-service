using ToDoService.Models;

namespace ToDoService.Repositories;

public class TodoRepository : ITodoRepository
{
    // In-memory array used as demo storage.
    private TodoItem[] _items = [];

    // Auto-incrementing id seed for new items.
    private int _nextId = 1;

    // Returns only items that are not soft-deleted.
    public TodoItem[] List()
    {
        return _items.Where(x => !x.IsDeleted).ToArray();
    }

    // Queries one active item by id.
    public TodoItem? Query(int id)
    {
        return _items.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
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

        Array.Resize(ref _items, _items.Length + 1);
        _items[^1] = newItem;

        return newItem;
    }

    // Edits an existing active item title.
    public TodoItem? Edit(int id, string title)
    {
        var item = _items.FirstOrDefault(x => x.Id == id);
        if (item is null || item.IsDeleted)
        {
            return null;
        }

        item.Title = title;
        return item;
    }

    // Soft-deletes an item by setting IsDeleted = true.
    public bool Delete(int id)
    {
        var item = _items.FirstOrDefault(x => x.Id == id);
        if (item is null)
        {
            return false;
        }

        item.IsDeleted = true;
        return true;
    }
}
