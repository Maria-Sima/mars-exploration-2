using Codecool.MarsExploration.MapExplorer.MapLoader;
using NUnit.Framework;

namespace MarsExploration2;

public class MarsExploration2
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        var loader = new MapLoader();
        var mapFile =
            "/home/maria/CodeCool/mars-exploration-2-csharp-Maria-Sima/Codecool.MarsExploration.MapExplorer/Resources/exploration-0.map";


        var map = loader.Load(mapFile);


        Assert.NotNull(map);
        Assert.True(map.SuccessfullyGenerated);
        TestContext.Out.WriteLine(map.ToString());
        Assert.That(map.Dimension, Is.EqualTo(32));
    }

    [Test]
    public void Test2()
    {
        var loader = new MapLoader();
        var mapFile =
            "/home/maria/CodeCool/mars-exploration-2-csharp-Maria-Sima/Codecool.MarsExploration.MapExplorer/Resources/exploration-1.map";

        // Act
        var map = loader.Load(mapFile);

        // Assert
        Assert.NotNull(map);
        Assert.True(map.SuccessfullyGenerated);
        TestContext.Out.WriteLine(map.ToString());
        Assert.That(map.Dimension, Is.EqualTo(32));
    }

    [Test]
    public void Test3()
    {
        var loader = new MapLoader();
        var mapFile =
            "/home/maria/CodeCool/mars-exploration-2-csharp-Maria-Sima/Codecool.MarsExploration.MapExplorer/Resources/exploration-2.map";

        // Act
        var map = loader.Load(mapFile);

        // Assert
        Assert.NotNull(map);
        Assert.True(map.SuccessfullyGenerated);
        TestContext.Out.WriteLine(map.ToString());
        Assert.That(map.Dimension, Is.EqualTo(32));
    }
}