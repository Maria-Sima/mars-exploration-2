using Codecool.MarsExploration.MapExplorer.Configuration;
using Codecool.MarsExploration.MapExplorer.MapLoader;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using NUnit.Framework;

namespace MarsExploration2Test;

public class ConfigurationValidatorTest
{
    private IConfigurationValidator _validator;

    public ConfigurationValidatorTest(IConfigurationValidator validator)
    {
        _validator = validator;
    }

    [SetUp]
    public void Setup()
    {
        IMapLoader mapLoader = new MapLoader();
        _validator = new ConfigurationValidator(mapLoader);
    }

    [Test]
    public void Test2()
    {
        var configuration = new Configuration
        {
            filePath = "",
            LandingCoordinate = new Coordinate(0, 0),
            ResourceSymbols = new List<string> { "%", "*", "&" },
            TimeoutSteps = 100
        };

        var result = _validator.Validate(configuration);
        TestContext.Out.WriteLine(result);
        Assert.That(result, Is.False);
    }

    [Test]
    public void Test3()
    {
        var configuration = new Configuration
        {
            filePath =
                @"D:\ProiecteCodecool\OOP C# Projects\mars-exploration-2-csharp-Maria-Sima\Codecool.MarsExploration.MapExplorer\Resources\exploration-0.map",
            LandingCoordinate = new Coordinate(3, 15),
            ResourceSymbols = new List<string> { "%", "*", "&" },
            TimeoutSteps = 100
        };

        var result = _validator.Validate(configuration);
        Assert.That(result, Is.True);
    }

    [Test]
    public void Test()
    {
        var configuration = new Configuration
        {
            filePath =
                @"D:\ProiecteCodecool\OOP C# Projects\mars-exploration-2-csharp-Maria-Sima\Codecool.MarsExploration.MapExplorer\Resources\exploration-0.map",
            LandingCoordinate = new Coordinate(0, 0),
            ResourceSymbols = new List<string> { "%", "*", "&" },
            TimeoutSteps = 100
        };

        var result = _validator.Validate(configuration);
        Assert.That(result, Is.True);
    }
}