using DotNetMcpServer.Core;
using DotNetMcpServer.Services;
using MCPSharp;

var quiet = !args.Contains("--debug");

try
{
    // Verify dotnet CLI is available
    if (!quiet) Console.WriteLine("Checking .NET CLI availability...");
    var isAvailable = await DotNetExecutor.IsAvailableAsync();
    if (!isAvailable)
    {
        Console.WriteLine("ERROR: .NET CLI is not available or not in PATH.");
        Console.WriteLine("Please ensure the .NET SDK is installed and accessible.");
        Environment.Exit(1);
    }

    // Get and display .NET version
    var versionResult = await DotNetExecutor.ExecuteAsync("--version");
    if (versionResult.IsSuccess && !quiet)
    {
        Console.WriteLine($".NET CLI Version: {versionResult.StandardOutput}");
    }

    // Register all command services with MCP Server
    if (!quiet) Console.WriteLine("Registering .NET CLI commands...");
    MCPServer.Register<DotNetCoreCommands>();
    
    if (!quiet) Console.WriteLine("Registering Entity Framework commands...");
    MCPServer.Register<EntityFrameworkCommands>();
    
    if (!quiet) Console.WriteLine("Registering Solution and NuGet commands...");
    MCPServer.Register<SolutionCommands>();

    // Start the MCP server
    if (!quiet) Console.WriteLine("Starting .NET MCP Server...");
    await MCPServer.StartAsync("DotNetMcpServer", "1.0.0");

    Console.WriteLine();
    Console.WriteLine("=== .NET MCP Server Started Successfully ===");
    Console.WriteLine("Available command categories:");
    Console.WriteLine("  • Core .NET CLI commands (build, run, test, publish, etc.)");
    Console.WriteLine("  • Entity Framework commands (migrations, database operations)");
    Console.WriteLine("  • Solution management (add/remove projects)");
    Console.WriteLine("  • NuGet package management");
    Console.WriteLine();
    Console.WriteLine("Press Enter to stop the server...");
    Console.ReadLine();
}
catch (Exception ex)
{
    Console.WriteLine($"ERROR: Failed to start .NET MCP Server: {ex.Message}");
    Environment.Exit(1);
}
