namespace AdventOfCode.Days;

internal class Day_03 : BaseDay
{
    private readonly char[][] _binaryInput;

    public Day_03()
    {
        _binaryInput = File.ReadAllLines(InputFilePath).Select(x => x.ToCharArray()).ToArray();
    }

    public override ValueTask<string> Solve_1()
    {
        var column = 0;
        var arrayWidth = _binaryInput[0].Length;
        var arrayHeight = _binaryInput.Length;
        var gammaRate = new StringBuilder();
        var epsilonRate = new StringBuilder();

        while (column < arrayWidth)
        {
            var bitCount = 0;

            for (int i = 0; i < arrayHeight; i++)
            {
                bitCount += _binaryInput[i][column] == '1' ? 1 : 0;
            }

            gammaRate.Append(bitCount > arrayHeight - bitCount ? "1" : "0");
            epsilonRate.Append(bitCount > arrayHeight - bitCount ? "0" : "1");

            column++;
        }

        return new($"{Convert.ToInt32(gammaRate.ToString(), 2) * Convert.ToInt32(epsilonRate.ToString(), 2)}");
    }

    public override ValueTask<string> Solve_2()
    {
        var column = 0;
        var arrayWidth = _binaryInput[0].Length;
        var oxygenGeneratorRatings = _binaryInput.ToList();
        var co2ScrubberRatings = _binaryInput.ToList();

        while (column < arrayWidth)
        {
            var bitCount = 0;

            for (int i = 0; i < oxygenGeneratorRatings.Count; i++)
            {
                bitCount += oxygenGeneratorRatings[i][column] == '1' ? 1 : 0;
            }

            var popularBit = bitCount >= oxygenGeneratorRatings.Count - bitCount ? '1' : '0';
            oxygenGeneratorRatings = oxygenGeneratorRatings.Count != 1 ? oxygenGeneratorRatings.Where(x => x[column] == popularBit).ToList() : oxygenGeneratorRatings;

            bitCount = 0;

            for (int i = 0; i < co2ScrubberRatings.Count; i++)
            {
                bitCount += co2ScrubberRatings[i][column] == '1' ? 1 : 0;
            }

            var leastPopularBit = bitCount < co2ScrubberRatings.Count - bitCount ? '1' : '0';
            co2ScrubberRatings = co2ScrubberRatings.Count != 1 ? co2ScrubberRatings.Where(x => x[column] == leastPopularBit).ToList() : co2ScrubberRatings;

            column++;
        }

        return new($"{Convert.ToInt32(new string(oxygenGeneratorRatings.First()), 2) * Convert.ToInt32(new string(co2ScrubberRatings.First()), 2)}");
    }
}
