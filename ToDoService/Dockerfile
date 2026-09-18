# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy project file and restore dependencies first for better build cache.
COPY ["ToDoService/ToDoService.csproj", "ToDoService/"]
RUN dotnet restore "ToDoService/ToDoService.csproj"

# Copy the rest of the source code and publish.
COPY . .
WORKDIR "/src/ToDoService"
RUN dotnet publish "ToDoService.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "ToDoService.dll"]
