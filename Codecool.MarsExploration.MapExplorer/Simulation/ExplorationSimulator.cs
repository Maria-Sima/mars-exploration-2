using Codecool.MarsExploration.MapExplorer.Configuration;
using Codecool.MarsExploration.MapExplorer.Exploration;
using Codecool.MarsExploration.MapExplorer.MapLoader;
using Codecool.MarsExploration.MapExplorer.MarsRover;

namespace Codecool.MarsExploration.MapExplorer.Simulation;

public class ExplorationSimulator
{
    public Configuration.Configuration _configuration;
    public IConfigurationValidator _configurationvalidator;
    public IMapLoader _mapLoader;
    public Rover _rover;
    public RoverDeployer _RoverDeployer;


    public ExplorationSimulator(IMapLoader mapLoader, IConfigurationValidator configurationvalidator, Rover rover,
        Configuration.Configuration configuration, RoverDeployer roverDeployer)
    {
        _mapLoader = mapLoader;
        _configurationvalidator = configurationvalidator;
        _rover = rover;
        _configuration = configuration;
        _RoverDeployer = roverDeployer;
    }

    public void Initialize(Configuration.Configuration configuration, IMapLoader mapLoader)
    {
        _configurationvalidator.Validate(configuration);
        _RoverDeployer.DeployRover();
    }

    public SimulationContext SimulationRun()
    {
        var steps = 0;
        var timeout = _configuration.TimeoutSteps;
        ExplorationOutcome explorationOutcome;
        while (steps <= timeout) steps++;

        if (steps == timeout) explorationOutcome = ExplorationOutcome.Timeout;


        return default;
    }
}