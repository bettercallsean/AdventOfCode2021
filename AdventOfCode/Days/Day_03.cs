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

            gammaRate.Append(bitCount >= arrayHeight - bitCount ? "1" : "0");
            epsilonRate.Append(bitCount >= arrayHeight - bitCount ? "0" : "1");

            column++;
        }

        return new($"{Convert.ToInt32(gammaRate.ToString(), 2) * Convert.ToInt32(epsilonRate.ToString(), 2)}");
    }

    public override ValueTask<string> Solve_2()
    {
        var column = 0;
        var arrayWidth = _binaryInput[0].Length;
        var oxygenGeneratorRatings = _binaryInput;
        var co2ScrubberRatings = _binaryInput;

        while (column < arrayWidth)
        {
            char mostPopularBit;

            mostPopularBit = GetMostPopularBit(column, oxygenGeneratorRatings);
            oxygenGeneratorRatings = oxygenGeneratorRatings.Length != 1 ? oxygenGeneratorRatings.Where(x => x[column] == mostPopularBit).ToArray() : oxygenGeneratorRatings;

            mostPopularBit = GetMostPopularBit(column, co2ScrubberRatings);
            co2ScrubberRatings = co2ScrubberRatings.Length != 1 ? co2ScrubberRatings.Where(x => x[column] != mostPopularBit).ToArray() : co2ScrubberRatings;

            column++;
        }

        return new($"{Convert.ToInt32(new string(oxygenGeneratorRatings[0]), 2) * Convert.ToInt32(new string(co2ScrubberRatings[0]), 2)}");
    }

    private char GetMostPopularBit(int column, char[][] ratingsList)
    {
        var bitCount = 0;

        for (int i = 0; i < ratingsList.Length; i++)
        {
            bitCount += ratingsList[i][column] == '1' ? 1 : 0;
        }

        return bitCount >= ratingsList.Length - bitCount ? '1' : '0';
    }
}
