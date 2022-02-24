namespace GoogleHashCode;

public static class EnvironmentConstants
{
    private static readonly Lazy<string> DataPathLazy = new(() => Path.Combine(TryGetSolutionDirectoryInfo().FullName, "Environment"));
    public static string DataPath => DataPathLazy.Value;

    private static DirectoryInfo TryGetSolutionDirectoryInfo(string currentPath = null)
    {

        var directory = new DirectoryInfo(
            currentPath ?? Directory.GetCurrentDirectory());
        while (directory != null && !directory.GetFiles("*.sln").Any())
        {
            directory = directory.Parent;
        }
        return directory;
    }

    public static string InputPath => Path.Combine(DataPath, "Input");
    public static string OutputPath => Path.Combine(DataPath, "Output");
}