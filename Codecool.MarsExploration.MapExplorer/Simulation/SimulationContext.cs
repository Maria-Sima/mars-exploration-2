using Codecool.MarsExploration.MapExplorer.Exploration;
using Codecool.MarsExploration.MapExplorer.MarsRover;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace Codecool.MarsExploration.MapExplorer.Simulation;

public record SimulationContext
{
    public ExplorationOutcome ExplorationOutcome;
    public Map map;
    public List<string> Resource;
    public Rover rover;
    public Coordinate SpaceshipCoordinate;
    public int Steps;
    public int TimeOutSteps;
}