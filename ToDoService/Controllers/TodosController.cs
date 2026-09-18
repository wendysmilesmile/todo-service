using Microsoft.AspNetCore.Mvc;
using ToDoService.Models;
using ToDoService.Services;

namespace ToDoService.Controllers;

[ApiController]
[Route("api/todo")]
public class TodosController : ControllerBase
{
    // Service layer used by this controller.
    private readonly ITodoService _service;
    private readonly ILogger<TodosController> _logger;

    public TodosController(ITodoService service, ILogger<TodosController> logger)
    {
        _service = service;
        _logger = logger;
    }

    // Returns all active (not soft-deleted) todo items.
    [HttpGet("list")]
    public ActionResult<TodoItem[]> List()
    {
        try
        {
            return Ok(_service.List());
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "List endpoint failed. Path: {Path}, QueryString: {QueryString}",
                HttpContext?.Request?.Path.Value,
                HttpContext?.Request?.QueryString.Value);

            return Ok(Array.Empty<TodoItem>());
        }
    }

    // Adds a new todo item.
    [HttpPost("add")]
    public ActionResult<TodoItem> Add([FromBody] AddTodoRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
        {
            return BadRequest("Title is required.");
        }

        var item = _service.Add(request.Title.Trim());
        return Ok(item);
    }

    // Soft-deletes a todo item by setting IsDeleted = true.
    [HttpPost("delete")]
    public IActionResult Delete([FromBody] DeleteTodoRequest request)
    {
        var deleted = _service.Delete(request.Id);
        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }

    // Request body for creating a todo item.
    public class AddTodoRequest
    {
        public string Title { get; set; } = string.Empty;
    }

    // Request body for deleting a todo item.
    public class DeleteTodoRequest
    {
        public int Id { get; set; }
    }
}
