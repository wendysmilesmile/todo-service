using Microsoft.EntityFrameworkCore;
using ToDoService.Data;
using ToDoService.Repositories;
using ToDoService.Services;

var builder = WebApplication.CreateBuilder(args);

// Register framework services and API features.
builder.Services.AddOpenApi();
builder.Services.AddControllers();

// Register application dependencies for DI.
var postgresConnectionString = builder.Configuration.GetConnectionString("Postgres")
    ?? "Host=localhost;Port=5432;Database=todoservice;Username=postgres;Password=postgres";

builder.Services.AddDbContext<TodoDbContext>(options =>
    options.UseNpgsql(postgresConnectionString));
builder.Services.AddScoped<ITodoRepository, EfCoreTodoRepository>();

builder.Services.AddScoped<ITodoService, TodoService>();

var app = builder.Build();

// Enable OpenAPI endpoint in development.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Redirect HTTP requests to HTTPS.
app.UseHttpsRedirection();

// Attempt to initialize the PostgreSQL database on startup.
try
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<TodoDbContext>();
    dbContext.Database.EnsureCreated();
}
catch
{
    // The app can still start even if PostgreSQL is temporarily unavailable.
}

// Map attribute-routed controllers.
app.MapControllers();

// Start the web application.
app.Run();
