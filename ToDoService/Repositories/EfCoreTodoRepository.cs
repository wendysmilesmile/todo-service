using ToDoService.Data;
using ToDoService.Models;

namespace ToDoService.Repositories;

// EF Core implementation of todo repository for SQLite/PostgreSQL.
public class EfCoreTodoRepository : ITodoRepository
{
    private readonly TodoDbContext _dbContext;

    public EfCoreTodoRepository(TodoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Returns active todo items ordered by id.
    public TodoItem[] List()
    {
        return _dbContext.TodoItems
            .Where(x => !x.IsDeleted)
            .OrderBy(x => x.Id)
            .ToArray();
    }

    // Queries one active todo item by id.
    public TodoItem? Query(int id)
    {
        return _dbContext.TodoItems.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
    }

    // Adds a new todo item and persists it.
    public TodoItem Add(string title)
    {
        var item = new TodoItem
        {
            Title = title,
            IsDeleted = false
        };

        _dbContext.TodoItems.Add(item);
        _dbContext.SaveChanges();

        return item;
    }

    // Edits an existing active todo item title.
    public TodoItem? Edit(int id, string title)
    {
        var item = _dbContext.TodoItems.FirstOrDefault(x => x.Id == id);
        if (item is null || item.IsDeleted)
        {
            return null;
        }

        item.Title = title;
        _dbContext.SaveChanges();

        return item;
    }

    // Soft-deletes an existing todo item.
    public bool Delete(int id)
    {
        var item = _dbContext.TodoItems.FirstOrDefault(x => x.Id == id);
        if (item is null)
        {
            return false;
        }

        item.IsDeleted = true;
        _dbContext.SaveChanges();

        return true;
    }
}
