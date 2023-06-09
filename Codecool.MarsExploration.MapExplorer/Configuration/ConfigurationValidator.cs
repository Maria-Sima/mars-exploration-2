using Codecool.MarsExploration.MapExplorer.MapLoader;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace Codecool.MarsExploration.MapExplorer.Configuration;

public class ConfigurationValidator : IConfigurationValidator
{
    private readonly IMapLoader _mapLoader;

    public ConfigurationValidator(IMapLoader mapLoader)
    {
        _mapLoader = mapLoader;
    }

    public bool Validate(Configuration configuration)
    {
        if (string.IsNullOrEmpty(configuration.filePath))
        {
            Console.WriteLine("Map file path is not provided.");
            return false;
        }

        var map = _mapLoader.Load(configuration.filePath);

        if (!map.IsEmpty(configuration.LandingCoordinate))
        {
            Console.WriteLine("The specified landing spot is occupied by an object on the map.");
            return false;
        }

        var hasAdjacentEmptySpot = HasAdjacentEmptySpot(configuration, map);

        if (!hasAdjacentEmptySpot)
        {
            Console.WriteLine("The landing spot does not have any adjacent empty spot to deploy the rover.");
            return false;
        }

        if (configuration.ResourceSymbols == null || configuration.ResourceSymbols.Count() == 0)
        {
            Console.WriteLine("At least one resource must be specified.");
            return false;
        }

        if (configuration.TimeoutSteps <= 0)
        {
            Console.WriteLine("The timeout must be greater than zero.");
            return false;
        }

        return true;
    }

    private static bool HasAdjacentEmptySpot(Configuration configuration, Map map)
    {
        var hasAdjacentEmptySpot = false;
        var x = configuration.LandingCoordinate.X;
        var y = configuration.LandingCoordinate.Y;
        var startX = Math.Max(x - 1, 0);
        var startY = Math.Max(y - 1, 0);
        var endX = Math.Min(x + 1, map.Dimension - 1);
        var endY = Math.Min(y + 1, map.Dimension - 1);

        for (var i = startX; i <= endX; i++)
        {
            for (var j = startY; j <= endY; j++)
            {
                if (i == x && j == y) continue;
                var coord = new Coordinate(i, j);
                if (map.IsEmpty(coord))
                {
                    hasAdjacentEmptySpot = true;
                    break;
                }
            }

            if (hasAdjacentEmptySpot) break;
        }


        return hasAdjacentEmptySpot;
    }
}