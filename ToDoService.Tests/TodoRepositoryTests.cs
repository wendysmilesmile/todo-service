using Moq;
using ToDoService.Repositories;

namespace ToDoService.Tests;

public class TodoRepositoryTests
{
    private readonly Mock<TodoRepository> _repositoryMock = new() { CallBase = true };

    private TodoRepository Repository => _repositoryMock.Object;

    [Fact]
    public void Add_ShouldCreateNewItem_WithIncrementedId_AndIsDeletedFalse()
    {
        var first = Repository.Add("Task A");
        var second = Repository.Add("Task B");

        Assert.Equal(1, first.Id);
        Assert.Equal(2, second.Id);
        Assert.False(first.IsDeleted);
        Assert.False(second.IsDeleted);
    }

    [Fact]
    public void List_ShouldReturnOnlyActiveItems()
    {
        Repository.Add("Task A");
        Repository.Add("Task B");

        var deleted = Repository.Delete(1);
        var items = Repository.List();

        Assert.True(deleted);
        Assert.Single(items);
        Assert.Equal(2, items[0].Id);
        Assert.Equal("Task B", items[0].Title);
    }

    [Fact]
    public void Delete_ShouldSoftDeleteItem_AndReturnTrue_WhenItemExists()
    {
        Repository.Add("Task A");

        var deleted = Repository.Delete(1);

        Assert.True(deleted);
        Assert.Empty(Repository.List());
    }

    [Fact]
    public void Delete_ShouldThrow_WhenItemDoesNotExist()
    {
        Assert.Throws<InvalidOperationException>(() => Repository.Delete(404));
    }

    [Fact]
    public void Delete_ShouldReturnTrue_WhenItemAlreadyDeleted()
    {
        Repository.Add("Task A");
        Repository.Delete(1);

        var deletedAgain = Repository.Delete(1);

        Assert.True(deletedAgain);
    }

    [Fact]
    public void Edit_ShouldUpdateTitle_WhenItemExists()
    {
        Repository.Add("Task A");

        var edited = Repository.Edit(1, "Task A Updated");

        Assert.NotNull(edited);
        Assert.Equal("Task A Updated", edited!.Title);
    }

    [Fact]
    public void Edit_ShouldThrow_WhenItemDoesNotExist()
    {
        Assert.Throws<InvalidOperationException>(() => Repository.Edit(999, "Task X"));
    }
}
