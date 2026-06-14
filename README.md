# To-Do List

A full-stack task management application built with **.NET 8**, **Blazor Server**, and **SQLite**.

## Features

- Create, edit, delete, and complete tasks
- Search tasks by title or description
- Set priority (Low / Medium / High)
- Dashboard with stats: pending tasks, completed today, high-priority items
- Quick-add widget on the homepage
- Dynamic greeting based on time of day
- Bootstrap 5 UI

## Tech Stack

| Layer | Technology |
|-------|-----------|
| Frontend | Blazor Server (Interactive) |
| Backend | ASP.NET Core Web API (in-process) |
| Database | SQLite via Entity Framework Core |
| ORM | Entity Framework Core 8 |
| Patterns | Repository, Service Layer, DTO |
| UI | Bootstrap 5 |

## Architecture

Single-project deployment — the API controllers and Blazor UI run in the same process. No separate API server needed.

```
ToDoList/
├── Controllers/      # REST API endpoints
├── Data/             # EF Core DbContext
├── DTOs/             # Request/response models
├── Models/           # Entity models
├── Repositories/     # Data access layer
├── Services/         # Business logic layer
├── Components/
│   ├── Pages/
│   │   ├── Home.razor      # Dashboard + Quick Add
│   │   └── Todos.razor     # Full task management
│   └── Layout/             # Nav menu + layout
└── Program.cs               # App entry point
```

## API Endpoints

| Method | Route | Description |
|--------|-------|-------------|
| GET | `/api/todo` | List all tasks (supports `?search=`) |
| GET | `/api/todo/{id}` | Get a single task |
| GET | `/api/todo/dashboard` | Get dashboard stats |
| POST | `/api/todo` | Create a task |
| PUT | `/api/todo/{id}` | Update a task |
| PATCH | `/api/todo/{id}/complete` | Mark task as completed |
| DELETE | `/api/todo/{id}` | Delete a task |

## Run Locally

```bash
cd ToDoList
dotnet run
```

Open `http://localhost:5xxx` in your browser.

## Deploy to Render (free)

1. Fork this repo
2. Go to https://render.com
3. Click **New +** → **Blueprint**
4. Select your repo
5. Click **Apply**

Render will build and deploy automatically.

## License

MIT
