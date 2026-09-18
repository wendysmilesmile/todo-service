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

    // Soft-deletes an existing todo item.
    public bool Delete(int id)
    {
        var item = _dbContext.TodoItems.FirstOrDefault(x => x.Id == id);
        if (item is null || item.IsDeleted)
        {
            return false;
        }

        item.IsDeleted = true;
        _dbContext.SaveChanges();

        return true;
    }
}
