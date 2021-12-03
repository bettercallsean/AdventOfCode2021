namespace AdventOfCode.Days;

internal class Day_03 : BaseDay
{
    private readonly char[][] _input;

    public Day_03()
    {
        _input = File.ReadAllLines(InputFilePath).Select(x => x.ToCharArray()).ToArray();
    }

    public override ValueTask<string> Solve_1()
    {
        var column = 0;
        var arrayWidth = _input[0].Length;
        var gammaRate = new StringBuilder();
        var epsilonRate = new StringBuilder();

        while (column < arrayWidth)
        {
            var mostPopularBit = GetMostPopularBit(column, _input);

            gammaRate.Append(mostPopularBit);
            epsilonRate.Append(mostPopularBit == '1' ? '0' : '1');

            column++;
        }

        return new($"{Convert.ToInt32(gammaRate.ToString(), 2) * Convert.ToInt32(epsilonRate.ToString(), 2)}");
    }

    public override ValueTask<string> Solve_2()
    {
        var column = 0;
        var arrayWidth = _input[0].Length;
        var oxygenGeneratorRatings = _input;
        var co2ScrubberRatings = _input;

        while (column < arrayWidth)
        {
            char mostPopularBit;

            if (oxygenGeneratorRatings.Length != 1)
            {
                mostPopularBit = GetMostPopularBit(column, oxygenGeneratorRatings);
                oxygenGeneratorRatings = oxygenGeneratorRatings.Where(x => x[column] == mostPopularBit).ToArray();
            }

            if (co2ScrubberRatings.Length != 1)
            {
                mostPopularBit = GetMostPopularBit(column, co2ScrubberRatings);
                co2ScrubberRatings = co2ScrubberRatings.Where(x => x[column] != mostPopularBit).ToArray();
            }

            column++;
        }

        return new($"{Convert.ToInt32(new string(oxygenGeneratorRatings[0]), 2) * Convert.ToInt32(new string(co2ScrubberRatings[0]), 2)}");
    }

    private char GetMostPopularBit(int column, char[][] binaryArray)
    {
        var bitCount = 0;

        for (int i = 0; i < binaryArray.Length; i++)
        {
            bitCount += binaryArray[i][column] == '1' ? 1 : 0;
        }

        return bitCount >= binaryArray.Length - bitCount ? '1' : '0';
    }
}
