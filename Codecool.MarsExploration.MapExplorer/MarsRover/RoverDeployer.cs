using Codecool.MarsExploration.MapExplorer.MapLoader;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorer.MarsRover;

public class RoverDeployer
{
    public static Configuration.Configuration _configuration;
    public static IMapLoader _mapLoader;
    public Rover _rover;


    public RoverDeployer(Configuration.Configuration configuration, IMapLoader mapLoader, Rover rover
    )
    {
        _configuration = configuration;
        _mapLoader = mapLoader;
        _rover = rover;
    }


    public void DeployRover()
    {
        var map = _mapLoader.Load(_configuration.filePath);

        var x = _configuration.LandingCoordinate.X;
        var y = _configuration.LandingCoordinate.Y;
        var startX = Math.Max(x - 1, 0);
        var startY = Math.Max(y - 1, 0);
        var endX = Math.Min(x + 1, map.Dimension - 1);
        var endY = Math.Min(y + 1, map.Dimension - 1);


        for (var i = startX; i <= endX; i++)
        for (var j = startY; j <= endY; j++)
        {
            var coord = new Coordinate(i, j);
            if (coord != _configuration.LandingCoordinate && map.IsEmpty(coord) && i > 0 && j > 0 &&
                i < map.Dimension && j < map.Dimension) _rover.CurrentPosition = coord;
        }
    }
}