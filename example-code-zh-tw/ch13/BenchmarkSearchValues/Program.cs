using BenchmarkDotNet.Running;
using BenchmarkSearchValues;

SetWorkingDirectoryToProjectRoot();
BenchmarkRunner.Run<SearchValuesBenchmarks>();

static void SetWorkingDirectoryToProjectRoot()
{
    string? projectRoot = FindProjectRoot(AppContext.BaseDirectory);
    if (projectRoot is not null)
    {
        Environment.CurrentDirectory = projectRoot;
    }
}

static string? FindProjectRoot(string startDirectory)
{
    DirectoryInfo? directory = new(startDirectory);
    while (directory is not null)
    {
        if (directory.GetFiles("*.csproj").Length > 0)
        {
            return directory.FullName;
        }

        directory = directory.Parent;
    }

    return null;
}
