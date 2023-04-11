using Codecool.MarsExploration.MapExplorer.Configuration;
using Codecool.MarsExploration.MapExplorer.MapLoader;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;

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


    public  void DeployRover()
    {
        Map map = _mapLoader.Load(_configuration.filePath);

        int x = _configuration.LandingCoordinate.X;
        int y = _configuration.LandingCoordinate.Y;
        int startX = Math.Max(0, x-1);
        int startY = Math.Max(0, y-1);
        int maxX=Math.Min(map.Dimension,x+1);
        int maxy=Math.Min(map.Dimension,y+1);
        

        for (int i = -startX; i <= maxX; i++)
        {
            for (int j = startY; j <= maxy; j++)
            {
                var coord = new Coordinate(i, j);
                if (coord != _configuration.LandingCoordinate && map.IsEmpty(coord) && i>0 && j>0 && i<map.Dimension && j<map.Dimension)
                {
                    _rover.CurrentPosition = coord;
                }
                
            }
        }
    }
}