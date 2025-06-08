using DotNetMcpServer.Services;
using MCPSharp;

namespace DotNetMcpServer.Tests;

public class SolutionCommandsTests
{
    public SolutionCommandsTests()
    {
        // Register commands before tests
        MCPServer.Register<SolutionCommands>();
    }

    [Fact]
    public async Task ListProjectsAsync_WithoutSolution_BuildsCorrectCommand()
    {
        // Act
        var result = await SolutionCommands.ListProjectsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Contains("dotnet sln list", result);
    }

    [Fact]
    public async Task ListProjectsAsync_WithSolution_BuildsCorrectCommand()
    {
        // Act
        var result = await SolutionCommands.ListProjectsAsync("MySolution.sln");

        // Assert
        Assert.NotNull(result);
        Assert.Contains("dotnet sln MySolution.sln list", result);
    }

    [Fact]
    public async Task AddProjectToSolutionAsync_WithProject_BuildsCorrectCommand()
    {
        // Act
        var result = await SolutionCommands.AddProjectToSolutionAsync("MyProject.csproj");

        // Assert
        Assert.NotNull(result);
        Assert.Contains("dotnet sln add MyProject.csproj", result);
    }

    [Fact]
    public async Task AddProjectToSolutionAsync_WithSolutionFolder_BuildsCorrectCommand()
    {
        // Act
        var result = await SolutionCommands.AddProjectToSolutionAsync(
            "MyProject.csproj", 
            "MySolution.sln", 
            "src");

        // Assert
        Assert.NotNull(result);
        var command = result.Split('\n')[0]; // Get the command line
        Assert.Contains("dotnet sln MySolution.sln add MyProject.csproj", command);
        Assert.Contains("--solution-folder src", command);
    }

    [Fact]
    public async Task RemoveProjectFromSolutionAsync_BuildsCorrectCommand()
    {
        // Act
        var result = await SolutionCommands.RemoveProjectFromSolutionAsync("MyProject.csproj");

        // Assert
        Assert.NotNull(result);
        Assert.Contains("dotnet sln remove MyProject.csproj", result);
    }

    [Fact]
    public async Task NuGetPushAsync_WithPackage_BuildsCorrectCommand()
    {
        // Act
        var result = await SolutionCommands.NuGetPushAsync("MyPackage.1.0.0.nupkg");

        // Assert
        Assert.NotNull(result);
        Assert.Contains("dotnet nuget push MyPackage.1.0.0.nupkg", result);
    }

    [Fact]
    public async Task NuGetPushAsync_WithAllParameters_BuildsCorrectCommand()
    {
        // Act
        var result = await SolutionCommands.NuGetPushAsync(
            "MyPackage.1.0.0.nupkg",
            "https://api.nuget.org/v3/index.json",
            "my-api-key",
            timeout: 300);

        // Assert
        Assert.NotNull(result);
        var command = result.Split('\n')[0]; // Get the command line
        Assert.Contains("dotnet nuget push MyPackage.1.0.0.nupkg", command);
        Assert.Contains("--source https://api.nuget.org/v3/index.json", command);
        Assert.Contains("--api-key my-api-key", command);
        Assert.Contains("--timeout 300", command);
    }

    [Fact]
    public async Task NuGetDeleteAsync_BuildsCorrectCommand()
    {
        // Act
        var result = await SolutionCommands.NuGetDeleteAsync("MyPackage", "1.0.0");

        // Assert
        Assert.NotNull(result);
        Assert.Contains("dotnet nuget delete MyPackage 1.0.0", result);
    }

    [Fact]
    public async Task NuGetLocalsAsync_Clear_BuildsCorrectCommand()
    {
        // Act
        var result = await SolutionCommands.NuGetLocalsAsync("all", clear: true);

        // Assert
        Assert.NotNull(result);
        Assert.Contains("dotnet nuget locals all --clear", result);
    }

    [Fact]
    public async Task NuGetLocalsAsync_List_BuildsCorrectCommand()
    {
        // Act
        var result = await SolutionCommands.NuGetLocalsAsync("global-packages", list: true);

        // Assert
        Assert.NotNull(result);
        Assert.Contains("dotnet nuget locals global-packages --list", result);
    }

    [Fact]
    public async Task NuGetAddSourceAsync_BuildsCorrectCommand()
    {
        // Act
        var result = await SolutionCommands.NuGetAddSourceAsync(
            "https://my-feed.com/nuget",
            "MyFeed");

        // Assert
        Assert.NotNull(result);
        var command = result.Split('\n')[0]; // Get the command line
        Assert.Contains("dotnet nuget add source https://my-feed.com/nuget", command);
        Assert.Contains("--name MyFeed", command);
    }

    [Fact]
    public async Task NuGetListSourceAsync_BuildsCorrectCommand()
    {
        // Act
        var result = await SolutionCommands.NuGetListSourceAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Contains("dotnet nuget list source", result);
    }

    [Fact]
    public async Task NuGetListSourceAsync_WithFormat_BuildsCorrectCommand()
    {
        // Act
        var result = await SolutionCommands.NuGetListSourceAsync("Detailed");

        // Assert
        Assert.NotNull(result);
        Assert.Contains("dotnet nuget list source --format Detailed", result);
    }
}
