namespace AdventOfCode.Days;

internal class Day_09 : BaseDay
{
    private readonly int[][] _input;
    private List<(int X, int Y)> _basins;
    private bool[][] _areaInBasin;

    public Day_09()
    {
        _input = File.ReadAllLines(InputFilePath).Select(x => x.ToCharArray().Select(x => x - '0').ToArray()).ToArray();
        _basins = new List<(int X, int Y)>();
        _areaInBasin = new bool[_input.GetLength(0)][];

        for (int i = 0; i < _areaInBasin.Length; i++)
        {
            _areaInBasin[i] = new bool[_input[0].Length];
        }
    }

    public override ValueTask<string> Solve_1()
    {
        var riskLevel = 0;

        for (int i = 0; i < _input.Length; i++)
        {
            for (int j = 0; j < _input[0].Length; j++)
            {
                var neighbours = new List<int>();

                if (i != 0)
                {
                    neighbours.Add(_input[i - 1][j]);
                }

                if (j != 0)
                {
                    neighbours.Add(_input[i][j - 1]);
                }

                if (i != _input.Length - 1)
                {
                    neighbours.Add(_input[i + 1][j]);
                }

                if (j != _input[0].Length - 1)
                {
                    neighbours.Add(_input[i][j + 1]);
                }

                if (neighbours.All(x => x > _input[i][j]))
                {
                    riskLevel += _input[i][j] + 1;
                    _basins.Add((i, j));
                }
            }
        }

        return new(riskLevel.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var largestBasins = new List<int>();

        foreach (var basin in _basins)
        {
            var basinSize = Foo(basin.X, basin.Y, 1);

            largestBasins.Add(basinSize);
        }

        return new(largestBasins.OrderByDescending(x => x).Take(3).Aggregate((x, y) => x * y).ToString());
    }

    private int Foo(int x, int y, int basinSize)
    {

        if (_input[x][y] == 9 || _areaInBasin[x][y])
        {
            return 0;
        }

        _areaInBasin[x][y] = true;

        // Up
        if (IsValidCoordinate(x - 1, y))
        {
            basinSize += Foo(x - 1, y, 1);
        }

        // Left
        if (IsValidCoordinate(x, y - 1))
        {
            basinSize += Foo(x, y - 1, 1);
        }

        // Right
        if (IsValidCoordinate(x, y + 1))
        {
            basinSize += Foo(x, y + 1, 1);
        }

        // Down
        if (IsValidCoordinate(x + 1, y))
        {
            basinSize += Foo(x + 1, y, 1);
        }

        return basinSize;
    }

    private bool IsValidCoordinate(int x, int y)
    {
        return !(x < 0 || y < 0 || x > _input.Length - 1 || y > _input[0].Length - 1);
    }
}
