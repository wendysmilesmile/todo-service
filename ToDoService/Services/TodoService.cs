using ToDoService.Models;
using ToDoService.Repositories;

namespace ToDoService.Services;

public class TodoService : ITodoService
{
    // Repository for in-memory todo persistence.
    private readonly ITodoRepository _repository;

    public TodoService(ITodoRepository repository)
    {
        _repository = repository;
    }

    // Gets all active todo items.
    public TodoItem[] List()
    {
        return _repository.List();
    }

    // Creates a new todo item.
    public TodoItem Add(string title)
    {
        return _repository.Add(title);
    }

    // Soft-deletes a todo item by id.
    public bool Delete(int id)
    {
        return _repository.Delete(id);
    }
}
