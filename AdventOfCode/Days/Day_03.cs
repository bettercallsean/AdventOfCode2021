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
        var columnIndex = 0;
        var arrayWidth = _input[0].Length;
        var gammaRate = new StringBuilder();
        var epsilonRate = new StringBuilder();

        while (columnIndex < arrayWidth)
        {
            var mostPopularBit = GetMostPopularBitInColumn(columnIndex, _input);

            gammaRate.Append(mostPopularBit);
            epsilonRate.Append(mostPopularBit == '1' ? '0' : '1');

            columnIndex++;
        }

        return new($"{Convert.ToInt32(gammaRate.ToString(), 2) * Convert.ToInt32(epsilonRate.ToString(), 2)}");
    }

    public override ValueTask<string> Solve_2()
    {
        var columnIndex = 0;
        var arrayWidth = _input[0].Length;
        var oxygenGeneratorRatings = _input;
        var co2ScrubberRatings = _input;

        while (columnIndex < arrayWidth)
        {
            char mostPopularBit;

            if (oxygenGeneratorRatings.Length != 1)
            {
                mostPopularBit = GetMostPopularBitInColumn(columnIndex, oxygenGeneratorRatings);
                oxygenGeneratorRatings = oxygenGeneratorRatings.Where(x => x[columnIndex] == mostPopularBit).ToArray();
            }

            if (co2ScrubberRatings.Length != 1)
            {
                mostPopularBit = GetMostPopularBitInColumn(columnIndex, co2ScrubberRatings);
                co2ScrubberRatings = co2ScrubberRatings.Where(x => x[columnIndex] != mostPopularBit).ToArray();
            }

            columnIndex++;
        }

        return new($"{Convert.ToInt32(new string(oxygenGeneratorRatings[0]), 2) * Convert.ToInt32(new string(co2ScrubberRatings[0]), 2)}");
    }

    private char GetMostPopularBitInColumn(int columnIndex, char[][] binaryArray)
    {
        var bitCount = 0;

        foreach (var binaryNumber in binaryArray)
        {
            bitCount += binaryNumber[columnIndex] == '1' ? 1 : 0;
        }

        return bitCount >= binaryArray.Length - bitCount ? '1' : '0';
    }
}
