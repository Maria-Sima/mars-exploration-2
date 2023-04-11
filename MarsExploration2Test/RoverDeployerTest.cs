using Codecool.MarsExploration.MapExplorer.Configuration;
using Codecool.MarsExploration.MapExplorer.MapLoader;
using Codecool.MarsExploration.MapExplorer.MarsRover;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using NUnit.Framework;

namespace MarsExploration2Test;

public class RoverDeployerTest
{
    private Configuration configuration;
    private IMapLoader mapLoader;
    private Rover rover;
    private RoverDeployer roverDeployer;

    [SetUp]
    public void Setup()
    {
        rover = new Rover();
        configuration = new Configuration
        {
            filePath =
                "/home/maria/CodeCool/mars-exploration-2-csharp-Maria-Sima/Codecool.MarsExploration.MapExplorer/Resources/exploration-0.map",
            LandingCoordinate = new Coordinate(5, 10),
            ResourceSymbols = new List<string> { "%", "*", "&" },
            TimeoutSteps = 100
        };
        mapLoader = new MapLoader();
        var map = mapLoader.Load(configuration.filePath);
        roverDeployer = new RoverDeployer(configuration, mapLoader, rover);
    }

    [Test]
    public void Test1()
    {
        roverDeployer.DeployRover();
        var roverPosition = rover.CurrentPosition;
        TestContext.Out.WriteLine(roverPosition);
        var adjacentCoordinates = new List<Coordinate>
            { new(5, 9), new(5, 11), new(4, 10), new(6, 10), new(6, 11), new(4, 11), new(4, 9), new(6, 9) };
        var result = adjacentCoordinates.Contains(roverPosition);
        Assert.That(result, Is.True);
    }
}