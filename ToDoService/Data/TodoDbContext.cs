using Microsoft.EntityFrameworkCore;
using ToDoService.Models;

namespace ToDoService.Data;

// EF Core database context for todo persistence.
public class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
    {
    }

    public DbSet<TodoItem> TodoItems => Set<TodoItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TodoItem>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(x => x.IsDeleted)
                .HasDefaultValue(false);
        });
    }
}
