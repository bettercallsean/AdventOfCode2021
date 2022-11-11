namespace AdventOfCode.Days;

internal class Day_08 : BaseDay
{
    private readonly string[] _signalPatterns;
    private readonly string[] _outputValues;

    public Day_08()
    {
        var input = File.ReadAllLines(InputFilePath);

        _signalPatterns = new string[input.Length];
        _outputValues = new string[input.Length];

        for (var i = 0; i < input.Length; i++)
        {
            var line = input[i];
            var data = line.Split(" | ");

            _signalPatterns[i] = data[0];
            _outputValues[i] = data[1];
        }
    }

    public override ValueTask<string> Solve_1()
    {
        // Key: The number itself
        // Value: Number of segments in a number
        var numberSegments = new Dictionary<int, int>
        {
            { 1, 2 },
            { 4, 4 },
            { 7, 3 },
            { 8, 7 }
        };

        var count = 0;

        foreach (var digit in _outputValues)
        {
            count += digit.Split(" ").Where(x => numberSegments.ContainsValue(x.Length)).Count();
        }

        return new(count.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var total = 0;

        for (var i = 0; i < _signalPatterns.Length; i++)
        {
            var signals = _signalPatterns[i].Split(" ").Select(x => string.Concat(x.OrderBy(i => i))).OrderBy(x => x.Length).ToArray();
            var charactersToNumber = new Dictionary<string, int>
            {
                { signals[0], 1 },
                { signals[1], 7 },
                { signals[2], 4 },
                { signals[9], 8 }
            };

            var six = signals.Where(x => x.Length == 6 && x.Intersect(signals[0]).Count() == 1).First();
            charactersToNumber.Add(six, 6);
            var three = signals.Where(x => x.Length == 5 && x.Intersect(signals[1]).Count() == 3).First();
            charactersToNumber.Add(three, 3);

            var fourAndThree = string.Concat(signals[2].Union(three));
            charactersToNumber.Add(signals.Where(x => x.Length == 6 && x.Intersect(fourAndThree).Count() == 6).First(), 9);
            charactersToNumber.Add(signals.Where(x => x.Length == 6 && !charactersToNumber.ContainsKey(x)).First(), 0);
            charactersToNumber.Add(signals.Where(x => x.Intersect(six).Count() == 5).First(), 5);
            charactersToNumber.Add(signals.Where(x => !charactersToNumber.ContainsKey(x)).First(), 2);

            var number = string.Empty;
            foreach (var output in _outputValues[i].Split(" ").Select(x => string.Concat(x.OrderBy(i => i))))
            {
                number = $"{number}{charactersToNumber[output]}";
            }

            total += int.Parse(number);
        }

        return new(total.ToString());
    }
}
