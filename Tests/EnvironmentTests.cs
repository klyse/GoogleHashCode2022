
namespace Tests;

public class EnvironmentTests
{
    [OneTimeSetUp]
    public void Setup()
    {
        if (Directory.Exists(EnvironmentConstants.OutputPath))
            Directory.Delete(EnvironmentConstants.OutputPath, true);
        Directory.CreateDirectory(EnvironmentConstants.OutputPath);

        Directory.CreateDirectory(EnvironmentConstants.InputPath);
    }

    [Test]
    public void CheckIfEnvironmentVariableIsSet()
    {
        Assert.IsNotNull(EnvironmentConstants.DataPath);
    }
}