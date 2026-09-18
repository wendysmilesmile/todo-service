# ToDoService

A demo ASP.NET Core Web API for managing a todo list with EF Core and PostgreSQL.

## Features

- List active todo items
- Add a new todo item
- Soft-delete a todo item (`IsDeleted = true`)
- Layered architecture: Controller -> Service -> Repository
- DI-based repository implementation with EF Core PostgreSQL

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
- PostgreSQL server

## Build

```bash
dotnet build ToDoService.sln
```

## Run

```bash
dotnet run --project ToDoService/ToDoService.csproj
```

PostgreSQL connection is configured in:

- [ToDoService/appsettings.json](ToDoService/appsettings.json)
- [ToDoService/appsettings.Development.json](ToDoService/appsettings.Development.json)

Default key:

```json
"ConnectionStrings": {
  "Postgres": "Host=localhost;Port=5432;Database=todoservice;Username=postgres;Password=postgres"
}
```

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

## CI/CD Reference (GitHub Actions)

This repository includes a complete CI/CD workflow:

- Workflow file: [.github/workflows/ci-cd.yml](.github/workflows/ci-cd.yml)
- Local compose file: [deploy/docker-compose.yml](deploy/docker-compose.yml)
- Production compose file: [deploy/docker-compose.prod.yml](deploy/docker-compose.prod.yml)

### CI (on pull request and push to `master`)

- Restore
- Build
- Lint (`dotnet format --verify-no-changes`)
- Unit tests
- Docker build validation

### CD (on push to `master`)

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
- `POSTGRES_DB`: PostgreSQL database name for production compose
- `POSTGRES_USER`: PostgreSQL username for production compose
- `POSTGRES_PASSWORD`: PostgreSQL password for production compose

`GITHUB_TOKEN` is provided automatically by GitHub Actions and is used by the workflow to push images to GHCR.

### First-time deployment notes

- Ensure Docker and Docker Compose are installed on the server.
- Ensure port `8080` is open on the server.
- Ensure the deployment user can run Docker commands.

## Notes

- Data is persisted in PostgreSQL.
- `delete` is a soft delete and does not physically remove array items.
