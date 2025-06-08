using DotNetMcpServer.Core;
using MCPSharp;

namespace DotNetMcpServer.Services;

/// <summary>
/// Provides solution and NuGet management commands as MCP tools
/// </summary>
public class SolutionCommands
{
    [McpTool("dotnet_sln_list", "Lists projects in a solution")]
    public static async Task<string> ListProjectsAsync(
        [McpParameter(false, "Solution file path")] string? solutionPath = null,
        [McpParameter(false, "Working directory")] string? workingDirectory = null)
    {
        var args = "sln";
        if (!string.IsNullOrEmpty(solutionPath))
            args += $" {solutionPath}";
        args += " list";

        var result = await DotNetExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpTool("dotnet_sln_add", "Adds projects to a solution")]
    public static async Task<string> AddProjectToSolutionAsync(
        [McpParameter(true, "Project path to add")] string projectPath,
        [McpParameter(false, "Solution file path")] string? solutionPath = null,
        [McpParameter(false, "Solution folder")] string? solutionFolder = null,
        [McpParameter(false, "Working directory")] string? workingDirectory = null)
    {
        var args = "sln";
        if (!string.IsNullOrEmpty(solutionPath))
            args += $" {solutionPath}";
        args += $" add {projectPath}";
        if (!string.IsNullOrEmpty(solutionFolder))
            args += $" --solution-folder {solutionFolder}";

        var result = await DotNetExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpTool("dotnet_sln_remove", "Removes projects from a solution")]
    public static async Task<string> RemoveProjectFromSolutionAsync(
        [McpParameter(true, "Project path to remove")] string projectPath,
        [McpParameter(false, "Solution file path")] string? solutionPath = null,
        [McpParameter(false, "Working directory")] string? workingDirectory = null)
    {
        var args = "sln";
        if (!string.IsNullOrEmpty(solutionPath))
            args += $" {solutionPath}";
        args += $" remove {projectPath}";

        var result = await DotNetExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpTool("nuget_push", "Pushes a package to a NuGet source")]
    public static async Task<string> NuGetPushAsync(
        [McpParameter(true, "Package file path")] string packagePath,
        [McpParameter(false, "NuGet source URL")] string? source = null,
        [McpParameter(false, "API key")] string? apiKey = null,
        [McpParameter(false, "Symbol source URL")] string? symbolSource = null,
        [McpParameter(false, "Symbol API key")] string? symbolApiKey = null,
        [McpParameter(false, "Timeout in seconds")] int? timeout = null,
        [McpParameter(false, "Working directory")] string? workingDirectory = null)
    {
        var args = $"nuget push {packagePath}";
        
        if (!string.IsNullOrEmpty(source))
            args += $" --source {source}";
        if (!string.IsNullOrEmpty(apiKey))
            args += $" --api-key {apiKey}";
        if (!string.IsNullOrEmpty(symbolSource))
            args += $" --symbol-source {symbolSource}";
        if (!string.IsNullOrEmpty(symbolApiKey))
            args += $" --symbol-api-key {symbolApiKey}";
        if (timeout.HasValue)
            args += $" --timeout {timeout.Value}";

        var result = await DotNetExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpTool("nuget_delete", "Deletes a package from a NuGet source")]
    public static async Task<string> NuGetDeleteAsync(
        [McpParameter(true, "Package name")] string packageName,
        [McpParameter(true, "Package version")] string version,
        [McpParameter(false, "NuGet source URL")] string? source = null,
        [McpParameter(false, "API key")] string? apiKey = null,
        [McpParameter(false, "Non-interactive mode")] bool nonInteractive = false,
        [McpParameter(false, "Working directory")] string? workingDirectory = null)
    {
        var args = $"nuget delete {packageName} {version}";
        
        if (!string.IsNullOrEmpty(source))
            args += $" --source {source}";
        if (!string.IsNullOrEmpty(apiKey))
            args += $" --api-key {apiKey}";
        if (nonInteractive)
            args += " --non-interactive";

        var result = await DotNetExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpTool("nuget_locals", "Clears or lists local NuGet caches")]
    public static async Task<string> NuGetLocalsAsync(
        [McpParameter(true, "Cache location (all, http-cache, global-packages, temp, plugins-cache)")] string cacheLocation,
        [McpParameter(false, "List cache contents instead of clearing")] bool list = false,
        [McpParameter(false, "Clear cache")] bool clear = false,
        [McpParameter(false, "Working directory")] string? workingDirectory = null)
    {
        var args = $"nuget locals {cacheLocation}";
        
        if (list)
            args += " --list";
        else if (clear)
            args += " --clear";

        var result = await DotNetExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpTool("nuget_add_source", "Adds a NuGet source")]
    public static async Task<string> NuGetAddSourceAsync(
        [McpParameter(true, "Source URL")] string source,
        [McpParameter(false, "Source name")] string? name = null,
        [McpParameter(false, "Username")] string? username = null,
        [McpParameter(false, "Password")] string? password = null,
        [McpParameter(false, "Store password in clear text")] bool storePasswordInClearText = false,
        [McpParameter(false, "Valid authentication types")] string? validAuthenticationTypes = null,
        [McpParameter(false, "Working directory")] string? workingDirectory = null)
    {
        var args = $"nuget add source {source}";
        
        if (!string.IsNullOrEmpty(name))
            args += $" --name {name}";
        if (!string.IsNullOrEmpty(username))
            args += $" --username {username}";
        if (!string.IsNullOrEmpty(password))
            args += $" --password {password}";
        if (storePasswordInClearText)
            args += " --store-password-in-clear-text";
        if (!string.IsNullOrEmpty(validAuthenticationTypes))
            args += $" --valid-authentication-types {validAuthenticationTypes}";

        var result = await DotNetExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpTool("nuget_remove_source", "Removes a NuGet source")]
    public static async Task<string> NuGetRemoveSourceAsync(
        [McpParameter(true, "Source name or URL")] string source,
        [McpParameter(false, "Working directory")] string? workingDirectory = null)
    {
        var args = $"nuget remove source {source}";

        var result = await DotNetExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpTool("nuget_list_source", "Lists NuGet sources")]
    public static async Task<string> NuGetListSourceAsync(
        [McpParameter(false, "Format (Detailed, Short)")] string? format = null,
        [McpParameter(false, "Working directory")] string? workingDirectory = null)
    {
        var args = "nuget list source";
        
        if (!string.IsNullOrEmpty(format))
            args += $" --format {format}";

        var result = await DotNetExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpTool("nuget_update_source", "Updates a NuGet source")]
    public static async Task<string> NuGetUpdateSourceAsync(
        [McpParameter(true, "Source name")] string name,
        [McpParameter(false, "Source URL")] string? source = null,
        [McpParameter(false, "Username")] string? username = null,
        [McpParameter(false, "Password")] string? password = null,
        [McpParameter(false, "Store password in clear text")] bool storePasswordInClearText = false,
        [McpParameter(false, "Valid authentication types")] string? validAuthenticationTypes = null,
        [McpParameter(false, "Working directory")] string? workingDirectory = null)
    {
        var args = $"nuget update source {name}";
        
        if (!string.IsNullOrEmpty(source))
            args += $" --source {source}";
        if (!string.IsNullOrEmpty(username))
            args += $" --username {username}";
        if (!string.IsNullOrEmpty(password))
            args += $" --password {password}";
        if (storePasswordInClearText)
            args += " --store-password-in-clear-text";
        if (!string.IsNullOrEmpty(validAuthenticationTypes))
            args += $" --valid-authentication-types {validAuthenticationTypes}";

        var result = await DotNetExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpTool("nuget_enable_source", "Enables a NuGet source")]
    public static async Task<string> NuGetEnableSourceAsync(
        [McpParameter(true, "Source name")] string name,
        [McpParameter(false, "Working directory")] string? workingDirectory = null)
    {
        var args = $"nuget enable source {name}";

        var result = await DotNetExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpTool("nuget_disable_source", "Disables a NuGet source")]
    public static async Task<string> NuGetDisableSourceAsync(
        [McpParameter(true, "Source name")] string name,
        [McpParameter(false, "Working directory")] string? workingDirectory = null)
    {
        var args = $"nuget disable source {name}";

        var result = await DotNetExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }
}
