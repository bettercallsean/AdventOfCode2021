namespace AdventOfCode.Days;

internal class Day_06 : BaseDay
{
    private readonly long[] _input;

    public Day_06()
    {
        var input = File.ReadAllText(InputFilePath).Split(',').Select(int.Parse).ToArray();

        _input = Enumerable.Range(0, 9).Select(i => Convert.ToInt64(input.Where(x => x == i).Count())).ToArray();
    }

    public override ValueTask<string> Solve_1()
    {
        var spawnNumbers = GetNumberOfSpawnsAfterXAmountOfDays(80);

        return new(spawnNumbers.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var spawnNumbers = GetNumberOfSpawnsAfterXAmountOfDays(256);

        return new(spawnNumbers.ToString());
    }

    private long GetNumberOfSpawnsAfterXAmountOfDays(int endDayCount)
    {
        var spawnRates = _input.ToArray();
        var dayCount = 0;

        while (dayCount < endDayCount)
        {
            var previousNumberOfSpawns = 0L;

            for (int i = spawnRates.Length - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    spawnRates[6] += spawnRates[i];
                    spawnRates[8] = spawnRates[i];
                }

                var tmp = spawnRates[i];

                spawnRates[i] = previousNumberOfSpawns;
                previousNumberOfSpawns = tmp;
            }

            dayCount++;
        }

        return spawnRates.Sum();
    }
}
