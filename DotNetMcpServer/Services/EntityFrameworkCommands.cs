using DotNetMcpServer.Core;
using MCPSharp;

namespace DotNetMcpServer.Services;

/// <summary>
/// Provides Entity Framework CLI commands as MCP tools
/// </summary>
public class EntityFrameworkCommands
{
    [McpTool("ef_database_update", "Updates the database to the latest migration")]
    public static async Task<string> UpdateDatabaseAsync(
        [McpParameter(false, "Target migration")] string? targetMigration = null,
        [McpParameter(false, "Connection string")] string? connection = null,
        [McpParameter(false, "DbContext class name")] string? context = null,
        [McpParameter(false, "Project path")] string? project = null,
        [McpParameter(false, "Startup project path")] string? startupProject = null,
        [McpParameter(false, "Working directory")] string? workingDirectory = null)
    {
        var args = "ef database update";
        
        if (!string.IsNullOrEmpty(targetMigration))
            args += $" {targetMigration}";
        if (!string.IsNullOrEmpty(connection))
            args += $" --connection \"{connection}\"";
        if (!string.IsNullOrEmpty(context))
            args += $" --context {context}";
        if (!string.IsNullOrEmpty(project))
            args += $" --project {project}";
        if (!string.IsNullOrEmpty(startupProject))
            args += $" --startup-project {startupProject}";

        var result = await DotNetExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpTool("ef_database_drop", "Drops the database")]
    public static async Task<string> DropDatabaseAsync(
        [McpParameter(false, "Force drop without confirmation")] bool force = false,
        [McpParameter(false, "Connection string")] string? connection = null,
        [McpParameter(false, "DbContext class name")] string? context = null,
        [McpParameter(false, "Project path")] string? project = null,
        [McpParameter(false, "Startup project path")] string? startupProject = null,
        [McpParameter(false, "Working directory")] string? workingDirectory = null)
    {
        var args = "ef database drop";
        
        if (force)
            args += " --force";
        if (!string.IsNullOrEmpty(connection))
            args += $" --connection \"{connection}\"";
        if (!string.IsNullOrEmpty(context))
            args += $" --context {context}";
        if (!string.IsNullOrEmpty(project))
            args += $" --project {project}";
        if (!string.IsNullOrEmpty(startupProject))
            args += $" --startup-project {startupProject}";

        var result = await DotNetExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpTool("ef_migrations_add", "Adds a new migration")]
    public static async Task<string> AddMigrationAsync(
        [McpParameter(true, "Migration name")] string name,
        [McpParameter(false, "Output directory for migration files")] string? outputDir = null,
        [McpParameter(false, "DbContext class name")] string? context = null,
        [McpParameter(false, "Project path")] string? project = null,
        [McpParameter(false, "Startup project path")] string? startupProject = null,
        [McpParameter(false, "Working directory")] string? workingDirectory = null)
    {
        var args = $"ef migrations add {name}";
        
        if (!string.IsNullOrEmpty(outputDir))
            args += $" --output-dir {outputDir}";
        if (!string.IsNullOrEmpty(context))
            args += $" --context {context}";
        if (!string.IsNullOrEmpty(project))
            args += $" --project {project}";
        if (!string.IsNullOrEmpty(startupProject))
            args += $" --startup-project {startupProject}";

        var result = await DotNetExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpTool("ef_migrations_remove", "Removes the last migration")]
    public static async Task<string> RemoveMigrationAsync(
        [McpParameter(false, "Force removal without checking for unapplied changes")] bool force = false,
        [McpParameter(false, "DbContext class name")] string? context = null,
        [McpParameter(false, "Project path")] string? project = null,
        [McpParameter(false, "Startup project path")] string? startupProject = null,
        [McpParameter(false, "Working directory")] string? workingDirectory = null)
    {
        var args = "ef migrations remove";
        
        if (force)
            args += " --force";
        if (!string.IsNullOrEmpty(context))
            args += $" --context {context}";
        if (!string.IsNullOrEmpty(project))
            args += $" --project {project}";
        if (!string.IsNullOrEmpty(startupProject))
            args += $" --startup-project {startupProject}";

        var result = await DotNetExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpTool("ef_migrations_list", "Lists available migrations")]
    public static async Task<string> ListMigrationsAsync(
        [McpParameter(false, "Connection string")] string? connection = null,
        [McpParameter(false, "DbContext class name")] string? context = null,
        [McpParameter(false, "Project path")] string? project = null,
        [McpParameter(false, "Startup project path")] string? startupProject = null,
        [McpParameter(false, "Working directory")] string? workingDirectory = null)
    {
        var args = "ef migrations list";
        
        if (!string.IsNullOrEmpty(connection))
            args += $" --connection \"{connection}\"";
        if (!string.IsNullOrEmpty(context))
            args += $" --context {context}";
        if (!string.IsNullOrEmpty(project))
            args += $" --project {project}";
        if (!string.IsNullOrEmpty(startupProject))
            args += $" --startup-project {startupProject}";

        var result = await DotNetExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpTool("ef_migrations_script", "Generates SQL script for migrations")]
    public static async Task<string> ScriptMigrationsAsync(
        [McpParameter(false, "From migration")] string? from = null,
        [McpParameter(false, "To migration")] string? to = null,
        [McpParameter(false, "Output file path")] string? output = null,
        [McpParameter(false, "Generate idempotent script")] bool idempotent = false,
        [McpParameter(false, "Connection string")] string? connection = null,
        [McpParameter(false, "DbContext class name")] string? context = null,
        [McpParameter(false, "Project path")] string? project = null,
        [McpParameter(false, "Startup project path")] string? startupProject = null,
        [McpParameter(false, "Working directory")] string? workingDirectory = null)
    {
        var args = "ef migrations script";
        
        if (!string.IsNullOrEmpty(from))
            args += $" {from}";
        if (!string.IsNullOrEmpty(to))
            args += $" {to}";
        if (!string.IsNullOrEmpty(output))
            args += $" --output {output}";
        if (idempotent)
            args += " --idempotent";
        if (!string.IsNullOrEmpty(connection))
            args += $" --connection \"{connection}\"";
        if (!string.IsNullOrEmpty(context))
            args += $" --context {context}";
        if (!string.IsNullOrEmpty(project))
            args += $" --project {project}";
        if (!string.IsNullOrEmpty(startupProject))
            args += $" --startup-project {startupProject}";

        var result = await DotNetExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpTool("ef_dbcontext_info", "Gets information about a DbContext type")]
    public static async Task<string> GetDbContextInfoAsync(
        [McpParameter(false, "DbContext class name")] string? context = null,
        [McpParameter(false, "Project path")] string? project = null,
        [McpParameter(false, "Startup project path")] string? startupProject = null,
        [McpParameter(false, "Working directory")] string? workingDirectory = null)
    {
        var args = "ef dbcontext info";
        
        if (!string.IsNullOrEmpty(context))
            args += $" --context {context}";
        if (!string.IsNullOrEmpty(project))
            args += $" --project {project}";
        if (!string.IsNullOrEmpty(startupProject))
            args += $" --startup-project {startupProject}";

        var result = await DotNetExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpTool("ef_dbcontext_list", "Lists available DbContext types")]
    public static async Task<string> ListDbContextsAsync(
        [McpParameter(false, "Project path")] string? project = null,
        [McpParameter(false, "Startup project path")] string? startupProject = null,
        [McpParameter(false, "Working directory")] string? workingDirectory = null)
    {
        var args = "ef dbcontext list";
        
        if (!string.IsNullOrEmpty(project))
            args += $" --project {project}";
        if (!string.IsNullOrEmpty(startupProject))
            args += $" --startup-project {startupProject}";

        var result = await DotNetExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpTool("ef_dbcontext_scaffold", "Scaffolds a DbContext and entity types from a database")]
    public static async Task<string> ScaffoldDbContextAsync(
        [McpParameter(true, "Database connection string")] string connectionString,
        [McpParameter(true, "Database provider (e.g., Microsoft.EntityFrameworkCore.SqlServer)")] string provider,
        [McpParameter(false, "Output directory")] string? outputDir = null,
        [McpParameter(false, "DbContext name")] string? contextName = null,
        [McpParameter(false, "DbContext directory")] string? contextDir = null,
        [McpParameter(false, "Force overwrite existing files")] bool force = false,
        [McpParameter(false, "Use data annotations instead of fluent API")] bool dataAnnotations = false,
        [McpParameter(false, "Project path")] string? project = null,
        [McpParameter(false, "Startup project path")] string? startupProject = null,
        [McpParameter(false, "Working directory")] string? workingDirectory = null)
    {
        var args = $"ef dbcontext scaffold \"{connectionString}\" {provider}";
        
        if (!string.IsNullOrEmpty(outputDir))
            args += $" --output-dir {outputDir}";
        if (!string.IsNullOrEmpty(contextName))
            args += $" --context {contextName}";
        if (!string.IsNullOrEmpty(contextDir))
            args += $" --context-dir {contextDir}";
        if (force)
            args += " --force";
        if (dataAnnotations)
            args += " --data-annotations";
        if (!string.IsNullOrEmpty(project))
            args += $" --project {project}";
        if (!string.IsNullOrEmpty(startupProject))
            args += $" --startup-project {startupProject}";

        var result = await DotNetExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpTool("ef_dbcontext_optimize", "Generates a compiled version of the model used by the DbContext")]
    public static async Task<string> OptimizeDbContextAsync(
        [McpParameter(false, "Output directory")] string? outputDir = null,
        [McpParameter(false, "Namespace for generated class")] string? nameSpace = null,
        [McpParameter(false, "DbContext class name")] string? context = null,
        [McpParameter(false, "Project path")] string? project = null,
        [McpParameter(false, "Startup project path")] string? startupProject = null,
        [McpParameter(false, "Working directory")] string? workingDirectory = null)
    {
        var args = "ef dbcontext optimize";
        
        if (!string.IsNullOrEmpty(outputDir))
            args += $" --output-dir {outputDir}";
        if (!string.IsNullOrEmpty(nameSpace))
            args += $" --namespace {nameSpace}";
        if (!string.IsNullOrEmpty(context))
            args += $" --context {context}";
        if (!string.IsNullOrEmpty(project))
            args += $" --project {project}";
        if (!string.IsNullOrEmpty(startupProject))
            args += $" --startup-project {startupProject}";

        var result = await DotNetExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpTool("ef_migrations_bundle", "Creates an executable bundle containing migrations")]
    public static async Task<string> BundleMigrationsAsync(
        [McpParameter(false, "Output file path")] string? output = null,
        [McpParameter(false, "Force overwrite existing bundle")] bool force = false,
        [McpParameter(false, "Self-contained bundle")] bool selfContained = false,
        [McpParameter(false, "Target runtime")] string? targetRuntime = null,
        [McpParameter(false, "DbContext class name")] string? context = null,
        [McpParameter(false, "Project path")] string? project = null,
        [McpParameter(false, "Startup project path")] string? startupProject = null,
        [McpParameter(false, "Working directory")] string? workingDirectory = null)
    {
        var args = "ef migrations bundle";
        
        if (!string.IsNullOrEmpty(output))
            args += $" --output {output}";
        if (force)
            args += " --force";
        if (selfContained)
            args += " --self-contained";
        if (!string.IsNullOrEmpty(targetRuntime))
            args += $" --target-runtime {targetRuntime}";
        if (!string.IsNullOrEmpty(context))
            args += $" --context {context}";
        if (!string.IsNullOrEmpty(project))
            args += $" --project {project}";
        if (!string.IsNullOrEmpty(startupProject))
            args += $" --startup-project {startupProject}";

        var result = await DotNetExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }
}
