namespace AdventOfCode.Days;

internal class Day_04 : BaseDay
{
    private readonly int[] _bingoNumbers;
    private readonly List<int[,]> _bingoBoards;
    private const int _boardLength = 5;

    public Day_04()
    {
        var data = File.ReadAllLines(InputFilePath);

        _bingoNumbers = data[0].Split(',').Select(int.Parse).ToArray();
        _bingoBoards = CreateBingoBoards(data.Skip(2).ToArray());
    }

    public override ValueTask<string> Solve_1()
    {
        var scores = GenerateScoresDictionary();
        var usedNumbers = new List<int>();
        var winningBoardNumber = -1;
        var lastNumberCalled = -1;

        foreach (var number in _bingoNumbers)
        {
            usedNumbers.Add(number);

            for (var boardNumber = 0; boardNumber < _bingoBoards.Count; boardNumber++)
            {
                var board = _bingoBoards[boardNumber];

                for (int i = 0; i < _boardLength; i++)
                {
                    for (int j = 0; j < _boardLength; j++)
                    {
                        if (board[i, j] == number)
                        {
                            scores[boardNumber]["row"][i]++;
                            scores[boardNumber]["column"][j]++;

                            if (scores[boardNumber]["row"][i] == 5 || scores[boardNumber]["column"][j] == 5)
                            {
                                winningBoardNumber = boardNumber;

                                i = _boardLength;
                                j = _boardLength;

                                break;
                            }
                        }
                    }
                }

                if (winningBoardNumber != -1)
                {
                    break;
                }
            }

            if (winningBoardNumber != -1)
            {
                lastNumberCalled = number;
                break;
            }
        }

        var winningBoard = _bingoBoards[winningBoardNumber];
        var score = CalculateBoardScore(winningBoard, usedNumbers);

        return new($"{score * lastNumberCalled}");
    }

    public override ValueTask<string> Solve_2()
    {
        var scores = GenerateScoresDictionary();
        var winningBoardsList = new List<int>();
        var usedNumbers = new List<int>();
        var lastWinningBoardFound = false;
        var lastWinningBoardNumber = -1;
        var lastNumberCalled = -1;

        foreach (var number in _bingoNumbers)
        {
            usedNumbers.Add(number);

            for (var boardNumber = 0; boardNumber < _bingoBoards.Count; boardNumber++)
            {
                var board = _bingoBoards[boardNumber];

                for (int i = 0; i < _boardLength; i++)
                {
                    for (int j = 0; j < _boardLength; j++)
                    {
                        if (board[i, j] == number && !winningBoardsList.Contains(boardNumber))
                        {
                            scores[boardNumber]["row"][i]++;
                            scores[boardNumber]["column"][j]++;

                            if (scores[boardNumber]["row"][i] == _boardLength || scores[boardNumber]["column"][j] == _boardLength)
                            {
                                lastWinningBoardNumber = boardNumber;
                                winningBoardsList.Add(boardNumber);

                                lastWinningBoardFound = winningBoardsList.Count == _bingoBoards.Count;

                                i = _boardLength;
                                j = _boardLength;

                                break;
                            }
                        }
                    }
                }

                if (lastWinningBoardFound)
                {
                    break;
                }
            }

            if (lastWinningBoardFound)
            {
                lastNumberCalled = number;
                break;
            }
        }

        var winningBoard = _bingoBoards[lastWinningBoardNumber];
        var score = CalculateBoardScore(winningBoard, usedNumbers);

        return new($"{score * lastNumberCalled}");
    }

    private List<int[,]> CreateBingoBoards(string[] boardData)
    {
        var bingoBoards = new List<int[,]>();
        var board = new int[_boardLength, _boardLength];
        var boardRow = 0;

        foreach (var boardLine in boardData)
        {
            if (string.IsNullOrWhiteSpace(boardLine)) continue;

            var numbers = boardLine.Split().Where(x => !string.IsNullOrEmpty(x)).Select(int.Parse).ToArray();

            for (int i = 0; i < numbers.Length; i++)
            {
                board[boardRow, i] = numbers[i];
            }

            boardRow++;

            if (boardRow == _boardLength)
            {
                bingoBoards.Add(board);

                board = new int[_boardLength, _boardLength];
                boardRow = 0;

                continue;
            }
        }

        return bingoBoards;
    }

    private List<Dictionary<string, int[]>> GenerateScoresDictionary()
    {
        var scores = new List<Dictionary<string, int[]>>();

        for (int i = 0; i < _bingoBoards.Count; i++)
        {
            scores.Add(new Dictionary<string, int[]> { { "row", new int[_boardLength] }, { "column", new int[_boardLength] } });
        }

        return scores;
    }

    private int CalculateBoardScore(int[,] board, List<int> usedNumbers)
    {
        var score = 0;

        for (int i = 0; i < _boardLength; i++)
        {
            for (int j = 0; j < _boardLength; j++)
            {
                if (!usedNumbers.Contains(board[i, j]))
                {
                    score += board[i, j];
                }
            }
        }

        return score;
    }
}
