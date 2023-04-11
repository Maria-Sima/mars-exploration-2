namespace Codecool.MarsExploration.MapGenerator.Calculators.Service;

public class DimensionCalculator : IDimensionCalculator
{
    public int CalculateDimension(int size, int dimensionGrowth)
    {
        var dimension = 0;
        var numberOfAvailableBoxes = 0;
        while (numberOfAvailableBoxes < size)
        {
            dimension++;
            numberOfAvailableBoxes = dimension * dimension;
        }

        return dimension + dimensionGrowth;
    }
}