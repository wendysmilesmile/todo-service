# ToDoService

A demo ASP.NET Core Web API for managing a todo list with in-memory storage.

## Features

- List active todo items
- Add a new todo item
- Soft-delete a todo item (`IsDeleted = true`)
- Layered architecture: Controller -> Service -> Repository

## Project Structure

- `ToDoService/` - Main Web API project
- `ToDoService.Tests/` - Unit test project (xUnit)

## API Endpoints

- `GET /api/todo/list`
- `POST /api/todo/add`
- `POST /api/todo/delete`

### Example Request Bodies

Add:

```json
{
  "title": "learn asp.net core"
}
```

Delete:

```json
{
  "id": 1
}
```

## Prerequisites

- .NET SDK 10.0+

## Build

```bash
dotnet build ToDoService.sln
```

## Run

```bash
dotnet run --project ToDoService/ToDoService.csproj
```

## Lint

This repository uses .NET analyzers and code style rules.

Check formatting and style:

```bash
dotnet format --verify-no-changes ToDoService.sln
```

Apply formatting:

```bash
dotnet format ToDoService.sln
```

## Unit Tests

Run tests:

```bash
dotnet test ToDoService.sln
```

## Notes

- Data is stored in memory and resets when the app restarts.
- `delete` is a soft delete and does not physically remove array items.
