namespace AdventOfCode.Days;

public class Day_05 : BaseDay
{
    private readonly int[][] _lines;
    private Dictionary<(int,int), int> _lineCoordinates;

    public Day_05()
    {
        _lines = File.ReadAllLines(InputFilePath)
            .Where(x => !string.IsNullOrEmpty(x))
            .Select(x => x.Replace(" -> ", ",").Split(",").Select(int.Parse).ToArray())
            .ToArray();
    }

    public override ValueTask<string> Solve_1()
    {
        _lineCoordinates = new Dictionary<(int,int), int>();
        var overlaps = 0;

        foreach(var line in _lines)
        {
            overlaps += FindOverlaps(line, true);
        }

        return new($"{overlaps}");
    }

    public override ValueTask<string> Solve_2()
    {
        _lineCoordinates = new Dictionary<(int,int), int>();
        var overlaps = 0;

        foreach (var line in _lines)
        {
            overlaps += FindOverlaps(line, false);
        }

        return new($"{overlaps}");
    }

    private int FindOverlaps(int[] line, bool ignoreDiagonals)
    {
        var overlaps = 0;
        var startingCoords = new int[] { line[0], line[1] };
        var endCoords = new int[] { line[2], line[3] };

        if(ignoreDiagonals && startingCoords[0] != endCoords[0] && startingCoords[1] != endCoords[1])
        {
            return overlaps;
        }

        var horizontalDirection = 0;
        var verticalDirection = 0;

        if (startingCoords[0] == endCoords[0])
        {
            verticalDirection = startingCoords[1] < endCoords[1] ? 1 : -1;
        }
        else if(startingCoords[1] == endCoords[1])
        {
            horizontalDirection = startingCoords[0] < endCoords[0] ? 1 : -1;
        }
        else
        {
            horizontalDirection = startingCoords[0] < endCoords[0] ? 1 : -1;
            verticalDirection = startingCoords[1] < endCoords[1] ? 1 : -1;
        }

        var x = startingCoords[0];
        var y = startingCoords[1];

        while (x != endCoords[0] + horizontalDirection || y != endCoords[1] + verticalDirection)
        {
            if (_lineCoordinates.ContainsKey((x,y)))
            {
                overlaps += ++_lineCoordinates[(x,y)] == 2 ? 1 : 0;
            }
            else
            {
                _lineCoordinates.Add((x,y), 1);
            }

            x += horizontalDirection;
            y += verticalDirection;
        }

        return overlaps;
    }
}