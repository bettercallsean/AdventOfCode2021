namespace AdventOfCode;

internal class Day_02 : BaseDay
{
    private readonly string[] _input;

    public Day_02()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        var horizontal = 0;
        var depth = 0;

        foreach (var instruction in _input)
        {
            var data = instruction.Split();
            var value = int.Parse(data[1]);

            if (data[0] == "forward")
            {
                horizontal += value;
            }
            else if (data[0] == "down")
            {
                depth += value;
            }
            else
            {
                depth -= value;
            }
        }

        return new($"{horizontal * depth}");
    }

    public override ValueTask<string> Solve_2()
    {
        var horizontal = 0;
        var depth = 0;
        var aim = 0;

        foreach (var instruction in _input)
        {
            var data = instruction.Split();
            var value = int.Parse(data[1]);

            if (data[0] == "forward")
            {
                horizontal += value;
                depth += aim * value;
            }
            else if (data[0] == "down")
            {
                aim += value;
            }
            else
            {
                aim -= value;
            }
        }

        return new($"{horizontal * depth}");
    }
}

