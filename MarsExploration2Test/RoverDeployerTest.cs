using System.Collections.Generic;
using Codecool.MarsExploration.MapExplorer.Configuration;
using Codecool.MarsExploration.MapExplorer.MapLoader;
using Codecool.MarsExploration.MapExplorer.MarsRover;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using NUnit.Framework;

namespace MarsExploration2Test
{
    public class RoverDeployerTest
    {
        private RoverDeployer roverDeployer;
        private IMapLoader mapLoader;
        private Configuration configuration;
        private Rover rover;

        [SetUp]
        public void Setup()
        {
            rover = new Rover();
            configuration = new Configuration {
                filePath = "/home/maria/CodeCool/mars-exploration-2-csharp-Maria-Sima/Codecool.MarsExploration.MapExplorer/Resources/exploration-0.map",
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
            var adjacentCoordinates = new List<Coordinate> { new Coordinate(5,9), new Coordinate(5, 11), new Coordinate(4, 10), new Coordinate(6, 10), new Coordinate(6, 11),new Coordinate(4, 11),new Coordinate(4, 9),new Coordinate(6, 9)};
            var result = adjacentCoordinates.Contains(roverPosition);
            Assert.That(result, Is.True);
        }
    }
}