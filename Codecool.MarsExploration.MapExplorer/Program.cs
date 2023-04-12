using Codecool.MarsExploration.MapExplorer.Configuration;
using Codecool.MarsExploration.MapExplorer.Logger;
using Codecool.MarsExploration.MapExplorer.MapLoader;
using Codecool.MarsExploration.MapExplorer.MarsRover;
using Codecool.MarsExploration.MapExplorer.Simulation;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorer;

internal class Program
{
    private static readonly string WorkDir = AppDomain.CurrentDomain.BaseDirectory;

    public static void Main(string[] args)
    {
        var mapFile = $@"{WorkDir}\Resources\exploration-0.map";
        var landingSpot = new Coordinate(6, 6);
        IMapLoader mapLoader = new MapLoader.MapLoader();
        Rover rover = new Rover();
        Configuration.Configuration configuration = new Configuration.Configuration
        {
            filePath =
                "/home/maria/CodeCool/mars-exploration-2-csharp-Maria-Sima/Codecool.MarsExploration.MapExplorer/Resources/exploration-0.map",
            LandingCoordinate = new Coordinate(16, 16),
            ResourceSymbols = new List<string> { "%", "*", "&" },
            TimeoutSteps = 100
        };;
        RoverDeployer roverDeployer = new RoverDeployer(configuration, mapLoader, rover);
        IConfigurationValidator configurationValidator = new ConfigurationValidator(mapLoader);
        ILogger logger = new ConsoleLogger();
        ExplorationSimulator explorationSimulator = new ExplorationSimulator(mapLoader, configurationValidator, rover,
            configuration, roverDeployer, logger);
       ;
        int necesary = 20;
        explorationSimulator.SimulationRun(rover,logger,necesary);
        
    }
}