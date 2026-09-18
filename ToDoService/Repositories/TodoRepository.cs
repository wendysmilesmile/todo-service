using ToDoService.Models;

namespace ToDoService.Repositories;

public class TodoRepository
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

    // Soft-deletes an item by setting IsDeleted = true.
    public bool Delete(int id)
    {
        var item = _items.FirstOrDefault(x => x.Id == id);
        if (item is null || item.IsDeleted)
        {
            return false;
        }

        item.IsDeleted = true;
        return true;
    }
}
