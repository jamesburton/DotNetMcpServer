using DotNetMcpServer.Core;
using MCPSharp;

namespace DotNetMcpServer.Services;

/// <summary>
/// Provides core .NET CLI commands as MCP tools
/// </summary>
public class DotNetCoreCommands
{
    [McpTool("dotnet_version", "Gets the installed .NET version")]
    public static async Task<string> GetVersionAsync()
    {
        var result = await DotNetExecutor.ExecuteAsync("--version");
        return result.ToString();
    }

    [McpTool("dotnet_info", "Displays .NET information")]
    public static async Task<string> GetInfoAsync()
    {
        var result = await DotNetExecutor.ExecuteAsync("--info");
        return result.ToString();
    }

    [McpTool("dotnet_list_sdks", "Lists installed .NET SDKs")]
    public static async Task<string> ListSdksAsync()
    {
        var result = await DotNetExecutor.ExecuteAsync("--list-sdks");
        return result.ToString();
    }

    [McpTool("dotnet_list_runtimes", "Lists installed .NET runtimes")]
    public static async Task<string> ListRuntimesAsync()
    {
        var result = await DotNetExecutor.ExecuteAsync("--list-runtimes");
        return result.ToString();
    }

    [McpTool("dotnet_new_list", "Lists available project templates")]
    public static async Task<string> ListTemplatesAsync()
    {
        var result = await DotNetExecutor.ExecuteAsync("new list");
        return result.ToString();
    }

    [McpTool("dotnet_new", "Creates a new project from template")]
    public static async Task<string> CreateProjectAsync(
        [McpParameter(true, "Template name (e.g., console, webapi, classlib)")] string template,
        [McpParameter(false, "Project name")] string? name = null,
        [McpParameter(false, "Output directory")] string? output = null,
        [McpParameter(false, "Target framework (e.g., net9.0)")] string? framework = null,
        [McpParameter(false, "Working directory")] string? workingDirectory = null)
    {
        var args = $"new {template}";
        
        if (!string.IsNullOrEmpty(name))
            args += $" --name {name}";
        if (!string.IsNullOrEmpty(output))
            args += $" --output {output}";
        if (!string.IsNullOrEmpty(framework))
            args += $" --framework {framework}";

        var result = await DotNetExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpTool("dotnet_restore", "Restores project dependencies")]
    public static async Task<string> RestoreAsync(
        [McpParameter(false, "Project path")] string? projectPath = null,
        [McpParameter(false, "Working directory")] string? workingDirectory = null)
    {
        var args = "restore";
        if (!string.IsNullOrEmpty(projectPath))
            args += $" {projectPath}";

        var result = await DotNetExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpTool("dotnet_build", "Builds a project")]
    public static async Task<string> BuildAsync(
        [McpParameter(false, "Project path")] string? projectPath = null,
        [McpParameter(false, "Build configuration (Debug/Release)")] string? configuration = null,
        [McpParameter(false, "Target framework")] string? framework = null,
        [McpParameter(false, "Working directory")] string? workingDirectory = null)
    {
        var args = "build";
        if (!string.IsNullOrEmpty(projectPath))
            args += $" {projectPath}";
        if (!string.IsNullOrEmpty(configuration))
            args += $" --configuration {configuration}";
        if (!string.IsNullOrEmpty(framework))
            args += $" --framework {framework}";

        var result = await DotNetExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpTool("dotnet_run", "Runs a project")]
    public static async Task<string> RunAsync(
        [McpParameter(false, "Project path")] string? projectPath = null,
        [McpParameter(false, "Build configuration (Debug/Release)")] string? configuration = null,
        [McpParameter(false, "Target framework")] string? framework = null,
        [McpParameter(false, "Working directory")] string? workingDirectory = null)
    {
        var args = "run";
        if (!string.IsNullOrEmpty(projectPath))
            args += $" --project {projectPath}";
        if (!string.IsNullOrEmpty(configuration))
            args += $" --configuration {configuration}";
        if (!string.IsNullOrEmpty(framework))
            args += $" --framework {framework}";

        var result = await DotNetExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpTool("dotnet_test", "Runs tests in a project")]
    public static async Task<string> TestAsync(
        [McpParameter(false, "Project path")] string? projectPath = null,
        [McpParameter(false, "Build configuration (Debug/Release)")] string? configuration = null,
        [McpParameter(false, "Target framework")] string? framework = null,
        [McpParameter(false, "Test filter expression")] string? filter = null,
        [McpParameter(false, "Skip building the project")] bool noBuild = false,
        [McpParameter(false, "Skip restoring packages")] bool noRestore = false,
        [McpParameter(false, "Working directory")] string? workingDirectory = null)
    {
        var args = "test";
        if (!string.IsNullOrEmpty(projectPath))
            args += $" {projectPath}";
        if (!string.IsNullOrEmpty(configuration))
            args += $" --configuration {configuration}";
        if (!string.IsNullOrEmpty(framework))
            args += $" --framework {framework}";
        if (!string.IsNullOrEmpty(filter))
            args += $" --filter {filter}";
        if (noBuild)
            args += " --no-build";
        if (noRestore)
            args += " --no-restore";

        var result = await DotNetExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpTool("dotnet_publish", "Publishes a project for deployment")]
    public static async Task<string> PublishAsync(
        [McpParameter(false, "Project path")] string? projectPath = null,
        [McpParameter(false, "Build configuration (Debug/Release)")] string? configuration = null,
        [McpParameter(false, "Target framework")] string? framework = null,
        [McpParameter(false, "Target runtime identifier")] string? runtime = null,
        [McpParameter(false, "Output directory")] string? output = null,
        [McpParameter(false, "Working directory")] string? workingDirectory = null)
    {
        var args = "publish";
        if (!string.IsNullOrEmpty(projectPath))
            args += $" {projectPath}";
        if (!string.IsNullOrEmpty(configuration))
            args += $" --configuration {configuration}";
        if (!string.IsNullOrEmpty(framework))
            args += $" --framework {framework}";
        if (!string.IsNullOrEmpty(runtime))
            args += $" --runtime {runtime}";
        if (!string.IsNullOrEmpty(output))
            args += $" --output {output}";

        var result = await DotNetExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpTool("dotnet_clean", "Cleans build outputs")]
    public static async Task<string> CleanAsync(
        [McpParameter(false, "Project path")] string? projectPath = null,
        [McpParameter(false, "Build configuration (Debug/Release)")] string? configuration = null,
        [McpParameter(false, "Target framework")] string? framework = null,
        [McpParameter(false, "Working directory")] string? workingDirectory = null)
    {
        var args = "clean";
        if (!string.IsNullOrEmpty(projectPath))
            args += $" {projectPath}";
        if (!string.IsNullOrEmpty(configuration))
            args += $" --configuration {configuration}";
        if (!string.IsNullOrEmpty(framework))
            args += $" --framework {framework}";

        var result = await DotNetExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpTool("dotnet_pack", "Creates NuGet packages")]
    public static async Task<string> PackAsync(
        [McpParameter(false, "Project path")] string? projectPath = null,
        [McpParameter(false, "Build configuration (Debug/Release)")] string? configuration = null,
        [McpParameter(false, "Output directory")] string? output = null,
        [McpParameter(false, "Working directory")] string? workingDirectory = null)
    {
        var args = "pack";
        if (!string.IsNullOrEmpty(projectPath))
            args += $" {projectPath}";
        if (!string.IsNullOrEmpty(configuration))
            args += $" --configuration {configuration}";
        if (!string.IsNullOrEmpty(output))
            args += $" --output {output}";

        var result = await DotNetExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpTool("dotnet_add_package", "Adds a NuGet package reference to a project")]
    public static async Task<string> AddPackageAsync(
        [McpParameter(true, "Package name")] string packageName,
        [McpParameter(false, "Package version")] string? version = null,
        [McpParameter(false, "Project path")] string? projectPath = null,
        [McpParameter(false, "Working directory")] string? workingDirectory = null)
    {
        var args = "add";
        if (!string.IsNullOrEmpty(projectPath))
            args += $" {projectPath}";
        args += $" package {packageName}";
        if (!string.IsNullOrEmpty(version))
            args += $" --version {version}";

        var result = await DotNetExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpTool("dotnet_remove_package", "Removes a NuGet package reference from a project")]
    public static async Task<string> RemovePackageAsync(
        [McpParameter(true, "Package name")] string packageName,
        [McpParameter(false, "Project path")] string? projectPath = null,
        [McpParameter(false, "Working directory")] string? workingDirectory = null)
    {
        var args = "remove";
        if (!string.IsNullOrEmpty(projectPath))
            args += $" {projectPath}";
        args += $" package {packageName}";

        var result = await DotNetExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpTool("dotnet_list_packages", "Lists package references in a project")]
    public static async Task<string> ListPackagesAsync(
        [McpParameter(false, "Project path")] string? projectPath = null,
        [McpParameter(false, "Include outdated packages")] bool outdated = false,
        [McpParameter(false, "Working directory")] string? workingDirectory = null)
    {
        var args = "list";
        if (!string.IsNullOrEmpty(projectPath))
            args += $" {projectPath}";
        args += " package";
        if (outdated)
            args += " --outdated";

        var result = await DotNetExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpTool("dotnet_add_reference", "Adds a project reference")]
    public static async Task<string> AddReferenceAsync(
        [McpParameter(true, "Reference project path")] string referencePath,
        [McpParameter(false, "Project path")] string? projectPath = null,
        [McpParameter(false, "Working directory")] string? workingDirectory = null)
    {
        var args = "add";
        if (!string.IsNullOrEmpty(projectPath))
            args += $" {projectPath}";
        args += $" reference {referencePath}";

        var result = await DotNetExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpTool("dotnet_remove_reference", "Removes a project reference")]
    public static async Task<string> RemoveReferenceAsync(
        [McpParameter(true, "Reference project path")] string referencePath,
        [McpParameter(false, "Project path")] string? projectPath = null,
        [McpParameter(false, "Working directory")] string? workingDirectory = null)
    {
        var args = "remove";
        if (!string.IsNullOrEmpty(projectPath))
            args += $" {projectPath}";
        args += $" reference {referencePath}";

        var result = await DotNetExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpTool("dotnet_list_references", "Lists project references")]
    public static async Task<string> ListReferencesAsync(
        [McpParameter(false, "Project path")] string? projectPath = null,
        [McpParameter(false, "Working directory")] string? workingDirectory = null)
    {
        var args = "list";
        if (!string.IsNullOrEmpty(projectPath))
            args += $" {projectPath}";
        args += " reference";

        var result = await DotNetExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }
}
