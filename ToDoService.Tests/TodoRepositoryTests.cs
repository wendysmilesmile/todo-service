using ToDoService.Repositories;

namespace ToDoService.Tests;

public class TodoRepositoryTests
{
    [Fact]
    public void Add_ShouldCreateNewItem_WithIncrementedId_AndIsDeletedFalse()
    {
        var repository = new TodoRepository();

        var first = repository.Add("Task A");
        var second = repository.Add("Task B");

        Assert.Equal(1, first.Id);
        Assert.Equal(2, second.Id);
        Assert.False(first.IsDeleted);
        Assert.False(second.IsDeleted);
    }

    [Fact]
    public void List_ShouldReturnOnlyActiveItems()
    {
        var repository = new TodoRepository();
        repository.Add("Task A");
        repository.Add("Task B");

        var deleted = repository.Delete(1);
        var items = repository.List();

        Assert.True(deleted);
        Assert.Single(items);
        Assert.Equal(2, items[0].Id);
        Assert.Equal("Task B", items[0].Title);
    }

    [Fact]
    public void Delete_ShouldSoftDeleteItem_AndReturnTrue_WhenItemExists()
    {
        var repository = new TodoRepository();
        repository.Add("Task A");

        var deleted = repository.Delete(1);

        Assert.True(deleted);
        Assert.Empty(repository.List());
    }

    [Fact]
    public void Delete_ShouldReturnFalse_WhenItemDoesNotExist()
    {
        var repository = new TodoRepository();

        var deleted = repository.Delete(404);

        Assert.False(deleted);
    }

    [Fact]
    public void Delete_ShouldReturnTrue_WhenItemAlreadyDeleted()
    {
        var repository = new TodoRepository();
        repository.Add("Task A");
        repository.Delete(1);

        var deletedAgain = repository.Delete(1);

        Assert.True(deletedAgain);
    }

    [Fact]
    public void Edit_ShouldUpdateTitle_WhenItemExists()
    {
        var repository = new TodoRepository();
        repository.Add("Task A");

        var edited = repository.Edit(1, "Task A Updated");

        Assert.NotNull(edited);
        Assert.Equal("Task A Updated", edited!.Title);
    }

    [Fact]
    public void Edit_ShouldReturnNull_WhenItemDoesNotExist()
    {
        var repository = new TodoRepository();

        var edited = repository.Edit(999, "Task X");

        Assert.Null(edited);
    }
}
