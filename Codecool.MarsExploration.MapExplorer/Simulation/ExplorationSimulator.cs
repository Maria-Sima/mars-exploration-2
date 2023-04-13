using System.Threading.Channels;
using Codecool.MarsExploration.MapExplorer.Configuration;
using Codecool.MarsExploration.MapExplorer.Exploration;
using Codecool.MarsExploration.MapExplorer.Logger;
using Codecool.MarsExploration.MapExplorer.MapLoader;
using Codecool.MarsExploration.MapExplorer.MarsRover;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace Codecool.MarsExploration.MapExplorer.Simulation;

public class ExplorationSimulator
{
    private Configuration.Configuration _configuration;
    private IConfigurationValidator _configurationvalidator;
    private IMapLoader _mapLoader;
    private Rover _rover;
    private RoverDeployer _RoverDeployer;
    private ILogger _consoleLogger;
    private List<Coordinate> PossibleMoves;
    private List<Coordinate> ExploredLocations = new List<Coordinate>();

    public ExplorationSimulator(IMapLoader mapLoader, IConfigurationValidator configurationvalidator, Rover rover,
        Configuration.Configuration configuration, RoverDeployer roverDeployer, ILogger consoleLogger)
    {
        _mapLoader = mapLoader;
        _configurationvalidator = configurationvalidator;
        _rover = rover;
        _configuration = configuration;
        _RoverDeployer = roverDeployer;
        _consoleLogger = consoleLogger;
    }

    private SimulationContext GenerateContext(Configuration.Configuration configuration, IMapLoader mapLoader,
        Rover rover, int timeout, int necesaryresources)
    {
        _RoverDeployer.DeployRover();

        return new SimulationContext
        {
            ExplorationOutcome = default,
            map = mapLoader.Load(configuration.filePath),
            Resource = new List<string> { "%", "*" },
            rover = rover,
            SpaceshipCoordinate = configuration.LandingCoordinate,
            Steps = 0,
            TimeOutSteps = timeout,
            NecesaryResources = necesaryresources
        };
    }

    public void SimulationRun(Rover rover, ILogger logger, int necesaryResources)
    {
        var context = GenerateContext(_configuration, _mapLoader, rover, _configuration.TimeoutSteps,
            necesaryResources);

        ExplorationOutcome explorationOutcome;
        while (context.Steps <= context.TimeOutSteps)
        {
            
            Scan(context.map, rover, _configuration);
            Move(rover, _configuration, context.map);
            Logger(logger, context, rover);
            Analysis(context);
            //SeeMap(context.map);
            context.Steps++;
            Console.ReadKey();
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"ExploratinOutcome Reached {context.ExplorationOutcome};  \n Found {_rover.WaterCoordinates.Count } water and {_rover.MineralCoordinates.Count} out of {context.NecesaryResources} needed");
    }

    private void SeeMap(Map map)
    {
        Console.BackgroundColor = ConsoleColor.Black;
        for (int i = 0; i < map.Dimension; i++)
        {
            for (int j = 0; j < map.Dimension; j++)
            {
                if (i == _rover.CurrentPosition.X && j == _rover.CurrentPosition.Y)
                {
                    
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(map.Representation[i, j]);
                    
                }
                
            }

            Console.WriteLine();
        }
    }

    private void Move(Rover rover, Configuration.Configuration configuration, Map map)
    {
        PossibleMoves = new List<Coordinate>();
        var x = rover.CurrentPosition.X;
        var y = rover.CurrentPosition.Y;
        var startX = Math.Max(x-1 , 0);
        var startY = Math.Max(y-1 , 0);
        var endX = Math.Min(x+1 , map.Dimension); 
        var endY = Math.Min(y+1 , map.Dimension);
        
        for (int i =startX ; i < endX; i++)
        {
            
            for (int j = startY; j < endY; j++)
            {
                
                if (map.Representation[i, j] == " " && !ExploredLocations.Contains(new Coordinate(i, j)))
                {
                    
                    PossibleMoves.Add(new Coordinate(i,j));
                }
            }
        }

        var rnd = new Random();
            if (PossibleMoves.Count>0)
                {
                    var NewPosition = PossibleMoves[rnd.Next(0,PossibleMoves.Count)];
                            _rover.CurrentPosition = NewPosition;
                            ExploredLocations.Add(NewPosition);
                }
       else if (PossibleMoves.Count==0)
        {
            _rover.CurrentPosition = ExploredLocations[rnd.Next(0,ExploredLocations.Count)];
        }

    
        
    }

    public void Scan(Map map, Rover rover, Configuration.Configuration configuration)
    {
        var x = rover.CurrentPosition.X;
        var y = rover.CurrentPosition.Y;
        var startX = Math.Max(x - rover.Sight, 0);
        var startY = Math.Max(y - rover.Sight, 0);
        var endX = Math.Min(x + rover.Sight, map.Dimension); 
        var endY = Math.Min(y + rover.Sight, map.Dimension);
        for (var i = startX; i < endX; i++)
        {
            for (var j = startY; j < endY; j++)
            {
                if (map.Representation[i, j] == "*" && !_rover.WaterCoordinates.Contains(new Coordinate(i,j)))

                {
                    rover.WaterCoordinates.Add(new Coordinate(i, j));
                    continue;
                }

                if (map.Representation[i, j] == "%" && !_rover.MineralCoordinates.Contains(new Coordinate(i,j)))
                {
                    rover.MineralCoordinates.Add(new Coordinate(i, j));
                    continue;
                }
                
            }
        }
    }

    private void Logger(ILogger consoleLogger, SimulationContext context, Rover rover)
    {
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        consoleLogger.Log($"STEP{context.Steps}; EVENT position; UNIT {rover.Id}; POSITION [{rover.CurrentPosition}]");
    }

    private ExplorationOutcome Analysis(SimulationContext context)
    {
        if (context.Steps <= context.TimeOutSteps && _rover.MineralCoordinates.Count + _rover.WaterCoordinates.Count >=
            context.NecesaryResources)
        {
            return ExplorationOutcome.Colonizable;
        }
        else if (context.Steps >= context.TimeOutSteps)
        {
            return ExplorationOutcome.Timeout;
        }

        return ExplorationOutcome.Error;
    }
}