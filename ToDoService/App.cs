using ToDoService.Repositories;
using ToDoService.Services;

var builder = WebApplication.CreateBuilder(args);

// Register framework services and API features.
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200", "http://127.0.0.1:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

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

// Allow browser requests from the frontend application.
app.UseCors("Frontend");

// Map attribute-routed controllers.
app.MapControllers();

// Start the web application.
app.Run();
