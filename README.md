# .NET MCP Server

A comprehensive Model Context Protocol (MCP) server that exposes .NET CLI commands and Entity Framework operations as MCP tools. This allows AI assistants and other MCP clients to interact with .NET projects through a standardized interface.

## Features

### Core .NET CLI Commands
- **Project Management**: Create, build, run, test, clean, and publish .NET projects
- **Package Management**: Add, remove, and list NuGet packages
- **Project References**: Manage project-to-project references
- **Template Operations**: List and create projects from templates
- **Information Commands**: Get .NET version, SDK info, and runtime details

### Entity Framework Commands
- **Database Operations**: Update, drop, and manage databases
- **Migration Management**: Add, remove, list, and script migrations
- **DbContext Operations**: List, scaffold, and optimize DbContext classes
- **Advanced Features**: Create migration bundles for deployment

### Solution & NuGet Management
- **Solution Operations**: Add/remove projects, list solution contents
- **NuGet Publishing**: Push and delete packages from NuGet sources
- **Source Management**: Add, remove, update, and list NuGet sources
- **Cache Management**: Clear and list NuGet local caches

## Prerequisites

- .NET 9.0 SDK or later
- Entity Framework CLI tools (for EF commands): `dotnet tool install --global dotnet-ef`

## Installation & Usage

1. **Clone and Build**:
   ```bash
   git clone <repository-url>
   cd DotNetMcpServer
   dotnet build
   ```

2. **Run the Server**:
   ```bash
   cd DotNetMcpServer
   dotnet run
   ```

3. **Run Tests**:
   ```bash
   dotnet test
   ```

## Available MCP Tools

### Core Commands

| Tool | Description | Required Parameters |
|------|-------------|-------------------|
| `dotnet_version` | Gets installed .NET version | None |
| `dotnet_info` | Displays detailed .NET information | None |
| `dotnet_list_sdks` | Lists installed .NET SDKs | None |
| `dotnet_list_runtimes` | Lists installed .NET runtimes | None |
| `dotnet_new_list` | Lists available project templates | None |
| `dotnet_new` | Creates new project from template | `template` (e.g., "console", "webapi") |
| `dotnet_restore` | Restores project dependencies | None |
| `dotnet_build` | Builds a project | None |
| `dotnet_run` | Runs a project | None |
| `dotnet_test` | Runs tests | None |
| `dotnet_publish` | Publishes project for deployment | None |
| `dotnet_clean` | Cleans build outputs | None |
| `dotnet_pack` | Creates NuGet packages | None |
| `dotnet_add_package` | Adds NuGet package reference | `packageName` |
| `dotnet_remove_package` | Removes NuGet package reference | `packageName` |
| `dotnet_list_packages` | Lists package references | None |
| `dotnet_add_reference` | Adds project reference | `referencePath` |
| `dotnet_remove_reference` | Removes project reference | `referencePath` |
| `dotnet_list_references` | Lists project references | None |

### Entity Framework Commands

| Tool | Description | Required Parameters |
|------|-------------|-------------------|
| `ef_database_update` | Updates database to latest migration | None |
| `ef_database_drop` | Drops the database | None |
| `ef_migrations_add` | Adds a new migration | `name` |
| `ef_migrations_remove` | Removes the last migration | None |
| `ef_migrations_list` | Lists available migrations | None |
| `ef_migrations_script` | Generates SQL script for migrations | None |
| `ef_migrations_bundle` | Creates executable migration bundle | None |
| `ef_dbcontext_info` | Gets DbContext information | None |
| `ef_dbcontext_list` | Lists available DbContext types | None |
| `ef_dbcontext_scaffold` | Scaffolds DbContext from database | `connectionString`, `provider` |
| `ef_dbcontext_optimize` | Generates compiled model | None |

### Solution & NuGet Commands

| Tool | Description | Required Parameters |
|------|-------------|-------------------|
| `dotnet_sln_list` | Lists projects in solution | None |
| `dotnet_sln_add` | Adds project to solution | `projectPath` |
| `dotnet_sln_remove` | Removes project from solution | `projectPath` |
| `nuget_push` | Pushes package to NuGet source | `packagePath` |
| `nuget_delete` | Deletes package from NuGet source | `packageName`, `version` |
| `nuget_locals` | Manages local NuGet caches | `cacheLocation` |
| `nuget_add_source` | Adds NuGet source | `source` |
| `nuget_remove_source` | Removes NuGet source | `source` |
| `nuget_list_source` | Lists NuGet sources | None |
| `nuget_update_source` | Updates NuGet source | `name` |
| `nuget_enable_source` | Enables NuGet source | `name` |
| `nuget_disable_source` | Disables NuGet source | `name` |

## Examples

### Creating a New Web API Project
```json
{
  "tool": "dotnet_new",
  "parameters": {
    "template": "webapi",
    "name": "MyWebApi",
    "framework": "net9.0",
    "workingDirectory": "/path/to/projects"
  }
}
```

### Adding Entity Framework Migration
```json
{
  "tool": "ef_migrations_add",
  "parameters": {
    "name": "InitialCreate",
    "context": "ApplicationDbContext",
    "project": "MyProject.csproj"
  }
}
```

### Building and Publishing Project
```json
{
  "tool": "dotnet_publish",
  "parameters": {
    "projectPath": "MyProject.csproj",
    "configuration": "Release",
    "runtime": "win-x64",
    "output": "./publish"
  }
}
```

## Architecture

The project is structured with clean separation of concerns:

- **Core/DotNetExecutor**: Handles process execution and command result management
- **Services/**: Contains MCP tool implementations organized by functionality
  - `DotNetCoreCommands`: Core .NET CLI operations
  - `EntityFrameworkCommands`: EF-specific operations
  - `SolutionCommands`: Solution and NuGet management
- **Tests/**: Comprehensive unit tests for all components

## Security Considerations

- All commands are executed in the context of the running user
- Working directory can be specified to limit scope
- No shell injection vulnerabilities - all parameters are properly escaped
- Commands run with the same permissions as the MCP server process

## Error Handling

- Comprehensive error capture from both stdout and stderr
- Execution time tracking for performance monitoring
- Graceful handling of missing dependencies
- Detailed error reporting with context information

## Contributing

1. Fork the repository
2. Create a feature branch
3. Add tests for new functionality
4. Ensure all tests pass
5. Submit a pull request

## License

This project is licensed under the MIT License - see the LICENSE file for details.
