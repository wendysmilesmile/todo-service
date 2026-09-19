using ToDoService.Repositories;
using ToDoService.Services;

namespace ToDoService.Tests;

public class TodoServiceTests
{
    [Fact]
    public void Add_And_List_ShouldReturnCreatedItem()
    {
        var repository = new TodoRepository();
        var service = new TodoService(repository);

        var created = service.Add("Service Task");
        var items = service.List();

        Assert.Equal(1, created.Id);
        Assert.Single(items);
        Assert.Equal("Service Task", items[0].Title);
    }

    [Fact]
    public void Delete_ShouldSoftDeleteItem_AndHideFromList()
    {
        var repository = new TodoRepository();
        var service = new TodoService(repository);
        service.Add("Task A");

        var deleted = service.Delete(1);
        var items = service.List();

        Assert.True(deleted);
        Assert.Empty(items);
    }

    [Fact]
    public void Edit_ShouldUpdateTitle_WhenItemExists()
    {
        var repository = new TodoRepository();
        var service = new TodoService(repository);
        service.Add("Task A");

        var edited = service.Edit(1, "Task A Updated");

        Assert.NotNull(edited);
        Assert.Equal("Task A Updated", edited!.Title);
    }

    [Fact]
    public void Delete_ShouldReturnFalse_WhenItemAlreadyDeleted()
    {
        var repository = new TodoRepository();
        var service = new TodoService(repository);
        service.Add("Task A");

        var firstDelete = service.Delete(1);
        var secondDelete = service.Delete(1);

        Assert.True(firstDelete);
        Assert.False(secondDelete);
    }
}
