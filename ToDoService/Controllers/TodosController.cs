using Microsoft.AspNetCore.Mvc;
using ToDoService.Models;
using ToDoService.Services;

namespace ToDoService.Controllers;

[ApiController]
[Route("api")]
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
    [HttpGet("todo-items")]
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

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new ErrorResponse
                {
                    ErrorCode = "500",
                    ErrorDescription = ex.Message
                });
        }
    }

    // Adds a new todo item.
    [HttpPost("todo-item")]
    public ActionResult<TodoItem> Add([FromBody] AddTodoRequest request)
    {
        var title = request.Title?.Trim();
        if (string.IsNullOrWhiteSpace(title))
        {
            return BadRequest("Title is required.");
        }

        var item = _service.Add(title);
        return Created($"/api/todo-item/{item.Id}", item);
    }

    // Edits a todo item title.
    [HttpPatch("todo-item/{id:int}")]
    public ActionResult<TodoItem> Edit(int id, [FromBody] EditTodoRequest request)
    {
        var title = request.Title?.Trim();
        if (string.IsNullOrWhiteSpace(title))
        {
            return BadRequest("Title is required.");
        }

        var item = _service.Edit(id, title);
        if (item is null)
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new ErrorResponse
                {
                    ErrorCode = "500",
                    ErrorDescription = $"Todo item {id} not found."
                });
        }

        return Ok(item);
    }

    // Soft-deletes a todo item by setting IsDeleted = true.
    [HttpDelete("todo-item/{id:int}")]
    public IActionResult Delete(int id)
    {
        var deleted = _service.Delete(id);
        if (!deleted)
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new ErrorResponse
                {
                    ErrorCode = "500",
                    ErrorDescription = $"Todo item {id} not found."
                });
        }

        return NoContent();
    }

}
