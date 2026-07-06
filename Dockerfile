# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files
COPY TaskTracker.sln ./
COPY TaskTracker.API/*.csproj TaskTracker.API/
COPY TaskTracker.Application/*.csproj TaskTracker.Application/
COPY TaskTracker.Infrastructure/*.csproj TaskTracker.Infrastructure/
COPY TaskTracker.Tests/*.csproj TaskTracker.Tests/

# Restore
RUN dotnet restore TaskTracker.sln

# Copy everything
COPY . .

# Publish
RUN dotnet publish TaskTracker.API/TaskTracker.API.csproj \
    -c Release \
    -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "TaskTracker.API.dll"]