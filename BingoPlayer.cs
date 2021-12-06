using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

public class BingoPlayer
{
    List<int> moves;
    List<BingoBoard> boards;

    public BingoPlayer()
    {
        moves = new List<int>();
        boards = new List<BingoBoard>();
    }

    public void ReadLines(string file)
    {
        var inputs = File.ReadAllLines("Input.txt").ToList();

        ReadMoves(inputs.First());

        int boardPos = 2;
        while (boardPos < inputs.Count)
        {
            ReadBoard(boardPos, inputs);
            boardPos += 6;
        }

        Console.WriteLine($"{boards.Count} boards.");
    }

    public void StartPlaying()
    {
        var bingo = false;

        for (var i = 0; i < moves.Count && !bingo; i++)
        {
            var move = moves[i];

            Console.WriteLine($"Calling out {move}.");

            foreach (var board in boards)
                board.MakeMove(move);

            for (var b = 0; b < boards.Count; b++)
            {
                var board = boards[b];
                var localBingo = board.BingoReached();

                if (localBingo)
                {
                    Console.WriteLine($"Bingo! for board {b}.");
                    bingo = true;

                    Console.WriteLine($"Res: {board.GetUnmarkedSum() * move}.");
                }
            }
        }

        Console.WriteLine("\nEnd of game.");
    }

    void ReadMoves(string moveLine)
    {
        var moveParts = moveLine.Split(',');
        foreach (var movePart in moveParts)
            moves.Add(int.Parse(movePart));

        Console.WriteLine($"{moves.Count} moves.");
    }

    void ReadBoard(int startIndex, List<string> lines)
    {
        var numbers = ReadLine(startIndex, lines)
            .Concat(ReadLine(startIndex + 1, lines))
            .Concat(ReadLine(startIndex + 2, lines))
            .Concat(ReadLine(startIndex + 3, lines))
            .Concat(ReadLine(startIndex + 4, lines))
            .ToList();

        boards.Add(new BingoBoard(numbers));
    }

    IEnumerable<int> ReadLine(int index, List<string> lines) 
    {
        return lines[index]
            .Split(' ')
            .Where(part => part.All(c => c >= '0' && c <= '9') && part.Length > 0)
            .Select(int.Parse);
    }
}