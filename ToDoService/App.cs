using ToDoService.Repositories;
using ToDoService.Services;

var builder = WebApplication.CreateBuilder(args);

// Register framework services and API features.
builder.Services.AddOpenApi();
builder.Services.AddControllers();

// Register application dependencies for DI.
builder.Services.AddSingleton<ITodoRepository, TodoRepository>();
builder.Services.AddScoped<ITodoService, TodoService>();

var app = builder.Build();

// Enable OpenAPI endpoint in development.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Redirect HTTP requests to HTTPS.
app.UseHttpsRedirection();

// Map attribute-routed controllers.
app.MapControllers();

// Start the web application.
app.Run();
