# TaskTracker API

A Task Tracker REST API built with **.NET 8 Clean Architecture**.

This project was developed as part of a Software Engineer (Backend Focused) take-home assignment.

---

# Features

- JWT Authentication
- User Registration & Login
- Role-Based Access Control (User/Admin)
- Task CRUD
- Pagination
- Search & Filtering
- FluentValidation
- SignalR Real-Time Notifications
- Swagger
- PostgreSQL
- Repository Pattern
- Global Exception Middleware
- Unit Tests

---

# Tech Stack

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL
- JWT Authentication
- SignalR
- FluentValidation
- xUnit
- Moq
- Swagger

---

# Architecture

```
TaskTracker.API
TaskTracker.Application
TaskTracker.Infrastructure
TaskTracker.Tests
```

Built using Clean Architecture.

---

# Getting Started

## Clone

```bash
git clone <repository-url>

cd TaskTracker
```

---

## Configure

Copy

```
appsettings.example.json
```

to

```
appsettings.json
```

Update

- PostgreSQL Connection String
- JWT Secret

---

## Database

```bash
dotnet ef database update \
--project TaskTracker.Infrastructure \
--startup-project TaskTracker.API
```

---

## Run

```bash
dotnet run --project TaskTracker.API
```

Swagger

```
https://localhost:xxxx/swagger
```

Replace **xxxx** with your local port.

---

# Authentication

Register

```
POST /api/auth/register
```

Login

```
POST /api/auth/login
```

Use the returned JWT token.

---

# API

## Tasks

```
POST /api/tasks

GET /api/tasks

GET /api/tasks/{id}

PUT /api/tasks/{id}

DELETE /api/tasks/{id}
```

Supports

- Pagination
- Search
- Status Filtering

---

# Real-Time

SignalR Hub

```
/hubs/tasks
```

Clients receive

- TaskCreated
- TaskUpdated
- TaskDeleted

---

# Testing

Run

```bash
dotnet test
```

Current Status

- 5 Unit Tests
- All Passing

---

# Postman

The repository contains

```
Postman/

TaskTracker.postman_collection.json

TaskTracker.postman_environment.json
```

---

# CI

GitHub Actions automatically

- Restore
- Build
- Test

on every Push and Pull Request.

---

# Future Improvements

- Docker Support
- Refresh Tokens
- Email Notifications
- Soft Delete
- Task Attachments

---

# License

For evaluation purposes only.
