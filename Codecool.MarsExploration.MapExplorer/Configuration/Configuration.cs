using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorer.Configuration;

public record Configuration
{
	public string filePath;
	public Coordinate LandingCoordinate;
	public IEnumerable<string> ResourceSymbols;
	public int TimeoutSteps;

};