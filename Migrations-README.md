# Database Migrations Guide

This guide explains how to add and apply migrations to the Agriculture Smart database.

## Prerequisites

- .NET SDK 8.0 or higher
- SQL Server instance

## Adding a New Migration

1. Open a terminal/command prompt
2. Navigate to the repository root directory
3. Run the following command to add a new migration:

```bash
dotnet ef migrations add [MigrationName] --project Agriculture_Smart_Dev_BE/AgricultureSmart.Repositories --startup-project Agriculture_Smart_Dev_BE/AgricultureSmart.API
```

Replace `[MigrationName]` with a descriptive name for your migration (e.g., `InitialCreate`, `AddUserTable`, etc.)

## Applying Migrations

Migrations will be automatically applied when the application starts, thanks to the `MigrationHelper` class.

If you want to apply migrations manually, you can use:

```bash
dotnet ef database update --project Agriculture_Smart_Dev_BE/AgricultureSmart.Repositories --startup-project Agriculture_Smart_Dev_BE/AgricultureSmart.API
```

## Reverting Migrations

To revert to a specific migration:

```bash
dotnet ef database update [MigrationName] --project Agriculture_Smart_Dev_BE/AgricultureSmart.Repositories --startup-project Agriculture_Smart_Dev_BE/AgricultureSmart.API
```

To revert all migrations:

```bash
dotnet ef database update 0 --project Agriculture_Smart_Dev_BE/AgricultureSmart.Repositories --startup-project Agriculture_Smart_Dev_BE/AgricultureSmart.API
```

## Removing the Last Migration

If you need to remove the last migration (before it's applied to the database):

```bash
dotnet ef migrations remove --project Agriculture_Smart_Dev_BE/AgricultureSmart.Repositories --startup-project Agriculture_Smart_Dev_BE/AgricultureSmart.API
```

## Generating SQL Script

To generate a SQL script for a migration without applying it:

```bash
dotnet ef migrations script [FromMigration] [ToMigration] --project Agriculture_Smart_Dev_BE/AgricultureSmart.Repositories --startup-project Agriculture_Smart_Dev_BE/AgricultureSmart.API --output migration.sql
``` 