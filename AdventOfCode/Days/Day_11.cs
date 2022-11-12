namespace AdventOfCode.Days;

internal class Day_11 : BaseDay
{
    private int[][] _input;
    private bool[][] _flashedOctopus;
    private Queue<(int X, int Y)> _octopusesToFlash = new();

    public Day_11()
    {
        _input = File.ReadAllLines(InputFilePath).Select(x => x.ToArray().Select(x => x - '0').ToArray()).ToArray();

        ResetFlashedOctopusArray();
    }

    public override ValueTask<string> Solve_1()
    {
        _input = File.ReadAllLines(InputFilePath).Select(x => x.ToArray().Select(x => x - '0').ToArray()).ToArray();

        var flashCount = 0;
        for (var i = 0; i < 100; i++)
        {
            for (var x = 0; x < _input.Length; x++)
            {
                for (var y = 0; y < _input[0].Length; y++)
                {

                    if (_input[x][y] == 9)
                    {
                        _input[x][y] = 0;
                        _flashedOctopus[x][y] = true;
                        _octopusesToFlash.Enqueue((x, y));
                    }
                    else
                    {
                        _input[x][y]++;
                    }
                }
            }

            while (_octopusesToFlash.Any())
            {
                var flashingOctopus = _octopusesToFlash.Dequeue();
                FlashSurroundingArea(flashingOctopus.X, flashingOctopus.Y);
            }

            flashCount += _input.SelectMany(x => x).Where(x => x == 0).Count();
            _octopusesToFlash = new Queue<(int X, int Y)>();

            ResetFlashedOctopusArray();
        }

        return new(flashCount.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        _input = File.ReadAllLines(InputFilePath).Select(x => x.ToArray().Select(x => x - '0').ToArray()).ToArray();

        var iteration = 0;
        var synchronousFlash = false;

        while (!synchronousFlash)
        {
            for (var x = 0; x < _input.Length; x++)
            {
                for (var y = 0; y < _input[0].Length; y++)
                {

                    if (_input[x][y] == 9)
                    {
                        _input[x][y] = 0;
                        _flashedOctopus[x][y] = true;
                        _octopusesToFlash.Enqueue((x, y));
                    }
                    else
                    {
                        _input[x][y]++;
                    }
                }
            }

            while (_octopusesToFlash.Any())
            {
                var flashingOctopus = _octopusesToFlash.Dequeue();
                FlashSurroundingArea(flashingOctopus.X, flashingOctopus.Y);
            }

            if (_input.SelectMany(x => x).All(x => x == 0))
            {
                synchronousFlash = true;
            }

            iteration++;
            _octopusesToFlash = new Queue<(int X, int Y)>();

            ResetFlashedOctopusArray();
        }

        return new(iteration.ToString());
    }

    private void FlashSurroundingArea(int x, int y)
    {
        var surroundingCoordinates = new List<(int X, int Y)>
        {
            (x - 1, y - 1), (x - 1, y), (x - 1, y + 1), (x, y - 1), (x, y + 1), (x + 1, y - 1), (x + 1, y), (x + 1, y + 1)
        };

        foreach (var coordinate in surroundingCoordinates)
        {
            if (ArrayHelper.IsValidCoordinate(coordinate.X, coordinate.Y, _input) && !_flashedOctopus[coordinate.X][coordinate.Y])
            {
                if (_input[coordinate.X][coordinate.Y] == 9)
                {
                    _input[coordinate.X][coordinate.Y] = 0;
                    _flashedOctopus[coordinate.X][coordinate.Y] = true;
                    _octopusesToFlash.Enqueue((coordinate.X, coordinate.Y));
                }
                else
                {
                    _input[coordinate.X][coordinate.Y]++;
                }
            }
        }
    }

    private void ResetFlashedOctopusArray()
    {
        _flashedOctopus = new bool[_input.GetLength(0)][];

        for (var i = 0; i < _flashedOctopus.Length; i++)
        {
            _flashedOctopus[i] = new bool[_input[0].Length];
        }
    }
}
