using Spectre.Console;

namespace AdventOfCode.Days;
internal class Day_10 : BaseDay
{
    private readonly char[][] _input;
    private readonly char[] _openingBrackets =
    {
        '(', '{', '[', '<',
    };
    private readonly char[] _closingBrackets =
    {
        ')', '}', ']', '>',
    };
    private readonly Dictionary<char, char> _closingBracketPairs = new Dictionary<char, char>
    {
        {')', '(' },
        {'}', '{' },
        {']', '[' },
        {'>', '<' }
    };

    private List<int> _corruptLines = new List<int>();

    public Day_10()
    {
        _input = File.ReadAllLines(InputFilePath).Select(x => x.ToCharArray().Select(x => x).ToArray()).ToArray();
    }

    public override ValueTask<string> Solve_1()
    {
        var bracketValues = new Dictionary<char, int>
        {
            { ')', 3 },
            { ']', 57 },
            { '}', 1197 },
            { '>', 25137 }
        };

        var corruptedBracketsTotal = 0;
        for (var i = 0; i < _input.Length; i++)
        {
            var line = _input[i];
            var openingBracketsIndex = new Stack<(char Bracket, int Index)>();

            for (var j = 0; j < line.Length; j++)
            {
                var bracket = line[j];
                if (_openingBrackets.Contains(bracket))
                {
                    openingBracketsIndex.Push((bracket, j));
                }
                else
                {
                    var lastOpeningBracket = openingBracketsIndex.First();
                    if (lastOpeningBracket.Bracket == _closingBracketPairs[bracket])
                    {
                        openingBracketsIndex.Pop();
                    }
                    else
                    {
                        corruptedBracketsTotal += bracketValues[bracket];
                        _corruptLines.Add(i);
                        break;
                    }
                }
            }
        }

        return new(corruptedBracketsTotal.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var bracketValues = new Dictionary<char, int>
        {
            { ')', 1 },
            { ']', 2 },
            { '}', 3 },
            { '>', 4 }
        };

        var openingBracketPairs = new Dictionary<char, char>
        {
            {'(', ')' },
            {'{', '}' },
            {'[', ']' },
            {'<', '>' }
        };

        var lineValues = new List<long>();

        for (var i = 0; i < _input.Length; i++)
        {
            long corruptedBracketsTotal = 0;
            if (_corruptLines.Contains(i))
            {
                continue;
            }

            var line = _input[i];
            var openingBracketsIndex = new Stack<(char Bracket, int Index)>();

            for (var j = 0; j < line.Length; j++)
            {
                var bracket = line[j];
                if (_openingBrackets.Contains(bracket))
                {
                    openingBracketsIndex.Push((bracket, j));
                }
                else
                {
                    var lastOpeningBracket = openingBracketsIndex.First();
                    if (lastOpeningBracket.Bracket == _closingBracketPairs[bracket])
                    {
                        openingBracketsIndex.Pop();
                    }
                }
            }

            foreach (var openingBracket in openingBracketsIndex)
            {
                corruptedBracketsTotal *= 5;
                var closingBracket = openingBracketPairs[openingBracket.Bracket];
                corruptedBracketsTotal += bracketValues[closingBracket];
            }

            lineValues.Add(corruptedBracketsTotal);
        }

        lineValues.Sort();
        var middleValue = lineValues[lineValues.Count() / 2];

        return new(middleValue.ToString());
    }
}
