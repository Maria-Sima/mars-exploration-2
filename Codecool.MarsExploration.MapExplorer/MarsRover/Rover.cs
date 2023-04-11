using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorer.MarsRover;

public class Rover
{
	public string Id { get; init; } = "rover-1";
	public Coordinate CurrentPosition { get; set; }
	public int Sight { get; init; } = 2;
	public Dictionary<string, List<Coordinate>> MineralCoordinates { get; set; }
	public Dictionary<string, List<Coordinate>> WaterCoordinates { get; set; }
}