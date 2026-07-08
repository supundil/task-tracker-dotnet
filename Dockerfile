# ==========================
# Build Stage
# ==========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# Copy solution
COPY TaskTracker.sln ./

# Copy project files
COPY TaskTracker.API/*.csproj TaskTracker.API/
COPY TaskTracker.Application/*.csproj TaskTracker.Application/
COPY TaskTracker.Infrastructure/*.csproj TaskTracker.Infrastructure/
COPY TaskTracker.Tests/*.csproj TaskTracker.Tests/

# Restore dependencies
RUN dotnet restore TaskTracker.sln

# Copy source code
COPY . .

# Publish API
RUN dotnet publish TaskTracker.API/TaskTracker.API.csproj \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

# ==========================
# Runtime Stage
# ==========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

COPY --from=build /app/publish .

# Render provides the PORT environment variable
ENV ASPNETCORE_URLS=http://+:8080

EXPOSE 8080

ENTRYPOINT ["dotnet", "TaskTracker.API.dll"]