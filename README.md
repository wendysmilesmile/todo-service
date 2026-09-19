# ToDoService

A demo ASP.NET Core Web API for managing a todo list with in-memory storage.

## Features

- List active todo items
- Add a new todo item
- Soft-delete a todo item (`IsDeleted = true`)
- Layered architecture: Controller -> Service -> Repository
- DI-based repository implementation with in-memory storage

## Project Structure

- `ToDoService/` - Main Web API project
- `ToDoService.Tests/` - Unit test project (xUnit)

## API Endpoints

- `GET /api/todo-items`
- `POST /api/todo-item`
- `PATCH /api/todo-item/{id}`
- `DELETE /api/todo-item/{id}`

### Example Request Bodies

Add:

```json
{
  "title": "do swimming"
}
```

Delete example request:

```text
DELETE /api/todo-item/1
```

Edit:

```json
{
  "title": "do swimming - updated"
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

The runtime repository is in-memory by default via DI registration in [ToDoService/App.cs](ToDoService/App.cs).

## Docker (One-Command Startup)

Build the image and start the service with one command:

```bash
docker compose -f deploy/docker-compose.yml up --build -d
```

API base URL after startup:

```text
http://localhost:8080
```

Stop and remove the container:

```bash
docker compose -f deploy/docker-compose.yml down
```

## Unit Tests

Run tests:

```bash
dotnet test ToDoService.sln
```

## CI/CD Reference (GitHub Actions)

This repository includes CI and a disabled CD template:

- Workflow file: [.github/workflows/ci-cd.yml](.github/workflows/ci-cd.yml)
- Local compose file: [deploy/docker-compose.yml](deploy/docker-compose.yml)
- Production compose file: [deploy/docker-compose.prod.yml](deploy/docker-compose.prod.yml)

### CI (on pull request and push to `master`)

- Restore
- Build
- Unit tests
- Docker build validation

### CD (currently disabled)

The CD job is commented out in [.github/workflows/ci-cd.yml](.github/workflows/ci-cd.yml).

To enable CD successfully:

1. Uncomment the `cd` job block in [.github/workflows/ci-cd.yml](.github/workflows/ci-cd.yml).
2. Configure the required GitHub Secrets.
3. Ensure your target server can run Docker and Docker Compose.
4. Ensure port `8080` is open.

When enabled, CD will:

- Build and push Docker image to GHCR (`ghcr.io/<owner>/<repo>`)
- Upload deployment compose file to server path `/opt/todoservice`
- Deploy with `docker compose` over SSH

### Required GitHub Secrets

Configure the following secrets in repository settings:

- `DEPLOY_HOST`: Target server host or IP
- `DEPLOY_USER`: SSH username on target server
- `DEPLOY_SSH_KEY`: Private SSH key for deployment user
- `GHCR_USERNAME`: GitHub username that can pull GHCR package
- `GHCR_TOKEN`: GitHub token with package read permission on server side

`GITHUB_TOKEN` is provided automatically by GitHub Actions and is used by the workflow to push images to GHCR.

### First-time deployment notes

- Ensure Docker and Docker Compose are installed on the server.
- Ensure port `8080` is open on the server.
- Ensure the deployment user can run Docker commands.

## Notes

- Data is stored in memory and resets when the app restarts.
- `delete` is a soft delete and does not physically remove array items.
