using System.IO;
using GoogleHashCode;
using NUnit.Framework;

namespace Tests;

public class EnvironmentTests
{
    [OneTimeSetUp]
    public void Setup()
    {
        if (!string.IsNullOrWhiteSpace(EnvironmentConstants.OutputPath))
            Directory.CreateDirectory(EnvironmentConstants.OutputPath);

        if (!string.IsNullOrWhiteSpace(EnvironmentConstants.InputPath))
            Directory.CreateDirectory(EnvironmentConstants.InputPath);
    }

    [Test]
    public void CheckIfEnvironmentVariableIsSet()
    {
        Assert.IsNotNull(EnvironmentConstants.DataPath);
    }
}