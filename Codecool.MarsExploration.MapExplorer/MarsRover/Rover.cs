using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorer.MarsRover;

public class Rover
{
    public string Id { get; init; } = "rover-1";
    public Coordinate CurrentPosition { get; set; }  
    public int Sight { get; init; } = 3;
    public List<Coordinate> MineralCoordinates = new List<Coordinate>();
    public List<Coordinate> WaterCoordinates = new List<Coordinate>();
}