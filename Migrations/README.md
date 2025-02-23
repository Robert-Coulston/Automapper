# Database Migrations

This folder contains the database migration files for the project.

## Commands

- Add a new migration:
  ```
  dotnet ef migrations add <MigrationName>
  ```

- Update the database:
  ```
  dotnet ef database update
  ```

- Remove the last migration:
  ```
  dotnet ef migrations remove
  ```

## Setup

1. Install the required packages:
  ```
  dotnet add package Microsoft.EntityFrameworkCore
  dotnet add package Microsoft.EntityFrameworkCore.Design
  dotnet add package Microsoft.EntityFrameworkCore.Sqlite
  ```

2. Configure the `DbContext` in your project.
