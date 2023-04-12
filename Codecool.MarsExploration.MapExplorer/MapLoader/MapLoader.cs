using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace Codecool.MarsExploration.MapExplorer.MapLoader;

public class MapLoader : IMapLoader
{
    public Map Load(string mapFile)
    {
        if (!File.Exists(mapFile)) throw new FileNotFoundException($"File {mapFile} does not exist.");

        var lines = File.ReadAllLines(mapFile);
        var map = new string?[lines.Length, lines[0].Length];

        for (var i = 0; i < lines.Length; i++)
        for (var j = 0; j < lines[i].Length; j++)
            if (lines[i][j] == ' ')
                map[i, j] = " ";
            else{
                map[i, j] = lines[i][j].ToString();}

        return new Map(map, true);
    }
}