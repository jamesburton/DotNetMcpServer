using DotNetMcpServer.Services;
using MCPSharp;

namespace DotNetMcpServer.Tests;

public class EntityFrameworkCommandsTests
{
    public EntityFrameworkCommandsTests()
    {
        // Register commands before tests
        MCPServer.Register<EntityFrameworkCommands>();
    }

    [Fact]
    public async Task UpdateDatabaseAsync_WithoutParameters_BuildsCorrectCommand()
    {
        // Act
        var result = await EntityFrameworkCommands.UpdateDatabaseAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Contains("dotnet ef database update", result);
    }

    [Fact]
    public async Task UpdateDatabaseAsync_WithParameters_BuildsCorrectCommand()
    {
        // Act
        var result = await EntityFrameworkCommands.UpdateDatabaseAsync(
            "InitialMigration", 
            "Server=.;Database=Test;", 
            "ApplicationDbContext",
            "MyProject.csproj");

        // Assert
        Assert.NotNull(result);
        var command = result.Split('\n')[0]; // Get the command line
        Assert.Contains("dotnet ef database update InitialMigration", command);
        Assert.Contains("--connection \"Server=.;Database=Test;\"", command);
        Assert.Contains("--context ApplicationDbContext", command);
        Assert.Contains("--project MyProject.csproj", command);
    }

    [Fact]
    public async Task AddMigrationAsync_WithName_BuildsCorrectCommand()
    {
        // Act
        var result = await EntityFrameworkCommands.AddMigrationAsync("InitialCreate");

        // Assert
        Assert.NotNull(result);
        Assert.Contains("dotnet ef migrations add InitialCreate", result);
    }

    [Fact]
    public async Task AddMigrationAsync_WithParameters_BuildsCorrectCommand()
    {
        // Act
        var result = await EntityFrameworkCommands.AddMigrationAsync(
            "AddUserTable", 
            "Migrations", 
            "ApplicationDbContext");

        // Assert
        Assert.NotNull(result);
        var command = result.Split('\n')[0]; // Get the command line
        Assert.Contains("dotnet ef migrations add AddUserTable", command);
        Assert.Contains("--output-dir Migrations", command);
        Assert.Contains("--context ApplicationDbContext", command);
    }

    [Fact]
    public async Task ScaffoldDbContextAsync_WithRequiredParameters_BuildsCorrectCommand()
    {
        // Act
        var result = await EntityFrameworkCommands.ScaffoldDbContextAsync(
            "Server=.;Database=MyDb;",
            "Microsoft.EntityFrameworkCore.SqlServer");

        // Assert
        Assert.NotNull(result);
        var command = result.Split('\n')[0]; // Get the command line
        Assert.Contains("dotnet ef dbcontext scaffold", command);
        Assert.Contains("\"Server=.;Database=MyDb;\"", command);
        Assert.Contains("Microsoft.EntityFrameworkCore.SqlServer", command);
    }

    [Fact]
    public async Task ScriptMigrationsAsync_WithIdempotent_BuildsCorrectCommand()
    {
        // Act
        var result = await EntityFrameworkCommands.ScriptMigrationsAsync(
            idempotent: true,
            output: "migration.sql");

        // Assert
        Assert.NotNull(result);
        var command = result.Split('\n')[0]; // Get the command line
        Assert.Contains("dotnet ef migrations script", command);
        Assert.Contains("--output migration.sql", command);
        Assert.Contains("--idempotent", command);
    }

    [Fact]
    public async Task ListMigrationsAsync_BuildsCorrectCommand()
    {
        // Act
        var result = await EntityFrameworkCommands.ListMigrationsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Contains("dotnet ef migrations list", result);
    }

    [Fact]
    public async Task GetDbContextInfoAsync_BuildsCorrectCommand()
    {
        // Act
        var result = await EntityFrameworkCommands.GetDbContextInfoAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Contains("dotnet ef dbcontext info", result);
    }

    [Fact]
    public async Task ListDbContextsAsync_BuildsCorrectCommand()
    {
        // Act
        var result = await EntityFrameworkCommands.ListDbContextsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Contains("dotnet ef dbcontext list", result);
    }
}
