using Moq;
using ToDoService.Repositories;
using ToDoService.Services;

namespace ToDoService.Tests;

public class TodoServiceTests
{
    private readonly Mock<TodoRepository> _repositoryMock = new() { CallBase = true };

    private TodoService CreateService()
    {
        return new TodoService(_repositoryMock.Object);
    }

    [Fact]
    public void Add_ShouldReturnCreatedItem()
    {
        var service = CreateService();

        var created = service.Add("Service Task");

        Assert.Equal(1, created.Id);
        Assert.Equal("Service Task", created.Title);
        Assert.False(created.IsDeleted);
    }

    [Fact]
    public void List_ShouldReturnItems()
    {
        var service = CreateService();
        service.Add("Task A");

        var items = service.List();

        Assert.Single(items);
        Assert.Equal("Task A", items[0].Title);
    }

    [Fact]
    public void Edit_ShouldReturnUpdatedItem_WhenItemExists()
    {
        var service = CreateService();
        service.Add("Task A");

        var edited = service.Edit(1, "Task A Updated");

        Assert.NotNull(edited);
        Assert.Equal("Task A Updated", edited!.Title);
    }

    [Fact]
    public void Delete_ShouldReturnFalse_WhenItemDoesNotExist()
    {
        var service = CreateService();

        var deleted = service.Delete(1);

        Assert.False(deleted);
    }

    [Fact]
    public void Delete_ShouldReturnTrue_WhenItemExists()
    {
        var service = CreateService();
        service.Add("Task A");

        var deleted = service.Delete(1);

        Assert.True(deleted);
    }
}
