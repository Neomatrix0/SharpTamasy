# SharpTamasy

SharpTamasy is a console-based task management application built with C# and .NET.
It lets you create, view, update, assign, and delete tasks from a local SQLite database.

The project supports different task types, including regular tasks, bug tasks, and feature tasks.
Bug tasks can have a severity level, while feature tasks can have a priority level.

## Features

- Create regular tasks, bug tasks, and feature tasks
- Store tasks in a local SQLite database
- View all saved tasks
- Search tasks by ID
- Update title, description, status, assignee, severity, and priority
- Delete existing tasks
- Automatically apply Entity Framework Core migrations on startup
- Unit tests for task creation and task service behavior

## Technologies

- C#
- .NET 10
- Entity Framework Core
- SQLite
- xUnit

## Project Structure

```text
SharpTamasy/
|-- Data/                 Database context
|-- Factories/            Task creation logic
|-- Migrations/           Entity Framework Core migrations
|-- Services/             Business logic
|-- SharpTamasy.Tests/    Unit tests
|-- Ui/                   Console menu
|-- Program.cs            Application entry point
`-- SharpTamasy.csproj    Main project file
```

## Getting Started

### Prerequisites

Install the .NET 10 SDK.

### Restore Dependencies

```bash
dotnet restore
```

### Build

```bash
dotnet build
```

### Run

```bash
dotnet run
```

When the application starts, it creates and updates the local SQLite database automatically.
The database file is generated locally as `tasks.db`.

### Run Tests

```bash
dotnet test
```

## Usage

After running the application, use the console menu to:

- Add a new task
- Display all tasks
- Search for a task by ID
- Delete a task
- Edit an existing task
- Exit the application
