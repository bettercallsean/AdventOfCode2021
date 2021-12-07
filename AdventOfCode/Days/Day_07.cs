namespace AdventOfCode.Days;

internal class Day_07 : BaseDay
{
    private readonly Dictionary<int, int> _input;
    public Day_07()
    {
        _input = File.ReadAllText(InputFilePath).Split(",").Select(int.Parse).GroupBy(x => x).Select(x => new
        {
            Pos = x.Key,
            Quantity = x.Count(y => y == x.Key)
        }).ToDictionary(x => x.Pos, x => x.Quantity);
    }

    public override ValueTask<string> Solve_1()
    {
        var lowestFuelCost = int.MaxValue;

        foreach (var position in _input)
        {
            var fuelCost = 0;

            foreach (var positionComparison in _input)
            {
                fuelCost += Math.Abs(positionComparison.Key - position.Key) * positionComparison.Value;
            }

            lowestFuelCost = fuelCost < lowestFuelCost ? fuelCost : lowestFuelCost;
        }

        return new(lowestFuelCost.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var lowerQuartile = Convert.ToInt32((_input.Count + 1) * .25);
        var upperQuartile = Convert.ToInt32((_input.Count + 1) * .75);
        var fuelCosts = Enumerable.Range(lowerQuartile - 1, upperQuartile + 1).ToArray();
        var lowestFuelCost = long.MaxValue;

        foreach (var horizontalPosition in fuelCosts)
        {
            var fuelCost = 0;

            foreach (var position in _input)
            {
                var horizontalDifference = Math.Abs(horizontalPosition - position.Key);
                fuelCost += (horizontalDifference * (horizontalDifference + 1) / 2) * position.Value;
            }

            lowestFuelCost = fuelCost < lowestFuelCost ? fuelCost : lowestFuelCost;
        }

        return new(lowestFuelCost.ToString());
    }
}
