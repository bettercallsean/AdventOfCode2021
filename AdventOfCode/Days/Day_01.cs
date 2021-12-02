namespace AdventOfCode;

public class Day_01 : BaseDay
{
    private readonly int[] _input;

    public Day_01()
    {
        _input = File.ReadAllLines(InputFilePath).Select(int.Parse).ToArray();
    }

    public override ValueTask<string> Solve_1()
    {
        var totalIncrements = 0;

        for (var i = 1; i < _input.Length; i++)
        {
            totalIncrements += _input[i] > _input[i - 1] ? 1 : 0;
        }

        return new(totalIncrements.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var previousIncrement = 0;
        var totalIncrements = 0;

        for (int i = 0; i < _input.Length - 3; i++)
        {
            var sum = _input[i] + _input[i + 1] + _input[i + 2];

            totalIncrements += sum > previousIncrement ? 1 : 0;
            previousIncrement = sum;
        }

        return new(totalIncrements.ToString());
    }
}
