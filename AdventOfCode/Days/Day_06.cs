namespace AdventOfCode.Days;

internal class Day_06 : BaseDay
{
    private readonly int[] _input;

    public Day_06()
    {
        _input = File.ReadAllText(InputFilePath).Split(',').Select(int.Parse).ToArray();
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
        var spawnRates = CreateSpawnRateArray();

        var dayCount = 0;

        while (dayCount < endDayCount)
        {
            var previousNumberOfSpawns = 0L;

            for (int i = spawnRates.Length - 1; i >= 0; i--)
            {
                if (i == 8)
                {
                    previousNumberOfSpawns = spawnRates[i];
                    spawnRates[8] = 0;
                }
                else if (i == 0)
                {
                    spawnRates[6] += spawnRates[i];
                    spawnRates[8] = spawnRates[i];

                    var tmp = spawnRates[i];

                    spawnRates[i] = previousNumberOfSpawns;
                    previousNumberOfSpawns = tmp;
                }
                else
                {
                    var tmp = spawnRates[i];

                    spawnRates[i] = previousNumberOfSpawns;
                    previousNumberOfSpawns = tmp;
                }
            }

            dayCount++;
        }

        return spawnRates.Sum();
    }

    private long[] CreateSpawnRateArray()
    {
        var spawns = new long[9];

        for (int i = 0; i < 9; i++)
        {
            spawns[i] = Convert.ToInt64(_input.Where(x => x == i).Count());
        }

        return spawns;
    }
}
