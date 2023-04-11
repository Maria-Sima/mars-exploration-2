using Codecool.MarsExploration.MapExplorer.MapLoader;
using Codecool.MarsExploration.MapExplorer.MarsRover;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace Codecool.MarsExploration.MapExplorer.Simulation;

public class RoverMovement
{
    public Configuration.Configuration _configuration;
    public IMapLoader _MapLoader;
    public Rover _Rover;
private List<Coordinate>

    public RoverMovement(IMapLoader mapLoader, Rover rover, Configuration.Configuration configuration)
    {
        _MapLoader = mapLoader;
        _Rover = rover;
        _configuration = configuration;
    }

    public void Move(Map map, Rover rover)
    {
    }

    public void Scan(Map map, Rover rover, Configuration.Configuration configuration)
    {
        
        var x = rover.CurrentPosition.X;
        var y = rover.CurrentPosition.Y;
        for (var i = x - rover.Sight; i < x + rover.Sight; i++)
        for (var j = y - rover.Sight; j < y + rover.Sight; j++)
            if (map.Representation[i, j] == "%")
                rover.MineralCoordinates.Add(new Coordinate(i, j));
            else if (map.Representation[i, j] == "%") rover.WaterCoordinates.Add(new Coordinate(i, j));
        
        
    }
}