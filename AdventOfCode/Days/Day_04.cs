namespace AdventOfCode.Days;

internal class Day_04 : BaseDay
{
    private readonly int[] _bingoNumbers;
    private readonly List<int[,]> _bingoBoards;
    private List<Dictionary<string, int[]>> _scores;

    public Day_04()
    {
        var data = File.ReadAllLines(InputFilePath);

        _bingoNumbers = data[0].Split(',').Select(int.Parse).ToArray();
        _bingoBoards = CreateBingoBoards(data);
    }

    public override ValueTask<string> Solve_1()
    {
        GenerateScoresDictionary();

        var winningBoardNumber = -1;
        var lastNumberCalled = 0;
        var usedNumbers = new List<int>();
        var arrayWidth = _bingoBoards[0].GetLength(0);
        var arrayHeight = _bingoBoards[0].GetLength(1);

        foreach (var number in _bingoNumbers)
        {
            usedNumbers.Add(number);

            for (var boardNumber = 0; boardNumber < _bingoBoards.Count; boardNumber++)
            {
                var board = _bingoBoards[boardNumber];

                for (int i = 0; i < arrayWidth; i++)
                {
                    for (int j = 0; j < arrayHeight; j++)
                    {
                        if (board[i, j] == number)
                        {
                            _scores[boardNumber]["row"][i]++;
                            _scores[boardNumber]["column"][j]++;

                            if (_scores[boardNumber]["row"][i] == 5 || _scores[boardNumber]["column"][j] == 5)
                            {
                                winningBoardNumber = boardNumber;

                                i = arrayWidth;
                                j = arrayHeight;

                                break;
                            }
                        }
                    }
                }
            }

            if (winningBoardNumber != -1)
            {
                lastNumberCalled = number;
                break;
            }
        }

        var winningBoard = _bingoBoards[winningBoardNumber];
        var score = GetBoardScore(winningBoard, usedNumbers);

        return new($"{score * lastNumberCalled}");
    }

    public override ValueTask<string> Solve_2()
    {
        GenerateScoresDictionary();

        var winningBoardsList = new List<int>();
        var lastWinningBoardFound = false;
        var lastWinningBoardNumber = 0;
        var lastNumberCalled = 0;
        var usedNumbers = new List<int>();
        var arrayWidth = _bingoBoards[0].GetLength(0);
        var arrayHeight = _bingoBoards[0].GetLength(1);

        foreach (var number in _bingoNumbers)
        {
            usedNumbers.Add(number);

            for (var boardNumber = 0; boardNumber < _bingoBoards.Count; boardNumber++)
            {
                if (lastWinningBoardFound)
                {
                    break;
                }

                var board = _bingoBoards[boardNumber];

                for (int i = 0; i < arrayWidth; i++)
                {
                    for (int j = 0; j < arrayHeight; j++)
                    {
                        if (board[i, j] == number && !winningBoardsList.Contains(boardNumber))
                        {
                            _scores[boardNumber]["row"][i]++;
                            _scores[boardNumber]["column"][j]++;

                            if (_scores[boardNumber]["row"][i] == 5 || _scores[boardNumber]["column"][j] == 5)
                            {
                                lastWinningBoardNumber = boardNumber;
                                winningBoardsList.Add(boardNumber);

                                lastWinningBoardFound = winningBoardsList.Count == _bingoBoards.Count;

                                i = arrayWidth;
                                j = arrayHeight;

                                break;
                            }
                        }
                    }
                }
            }

            if (lastWinningBoardFound)
            {
                lastNumberCalled = number;
                break;
            }
        }

        var winningBoard = _bingoBoards[lastWinningBoardNumber];
        var score = GetBoardScore(winningBoard, usedNumbers);

        return new($"{score * lastNumberCalled}");
    }

    private List<int[,]> CreateBingoBoards(string[] boardData)
    {
        var bingoBoards = new List<int[,]>();
        var board = new int[5, 5];
        var boardRow = 0;

        foreach (var boardLine in boardData.Skip(2))
        {
            if (string.IsNullOrWhiteSpace(boardLine)) continue;

            var numbers = boardLine.Split().Where(x => !string.IsNullOrEmpty(x)).Select(int.Parse).ToArray();

            for (int i = 0; i < numbers.Length; i++)
            {
                board[boardRow, i] = numbers[i];
            }

            boardRow++;

            if (boardRow == 5)
            {
                bingoBoards.Add(board);

                board = new int[5, 5];
                boardRow = 0;

                continue;
            }
        }

        return bingoBoards;
    }

    private void GenerateScoresDictionary()
    {
        _scores = new List<Dictionary<string, int[]>>();

        for (int i = 0; i < _bingoBoards.Count; i++)
        {
            _scores.Add(new Dictionary<string, int[]> { { "row", new int[5] }, { "column", new int[5] } });
        }
    }

    private int GetBoardScore(int[,] board, List<int> usedNumbers)
    {
        var score = 0;

        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
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
