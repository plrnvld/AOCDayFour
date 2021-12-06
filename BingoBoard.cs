using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

public class BingoBoard
{
    List<int> numbers;
    List<int> marked;

    public BingoBoard(IEnumerable<int> nums)
    {
        if (nums.Count() != 25)
            throw new Exception($"Broken board, Length={nums.Count()}.");

        numbers = new List<int>(nums);
        marked = new List<int>();
    }

    public void MakeMove(int move)
    {
        marked.Add(move);
    }

    public bool BingoReached()
    {
        var lineRange = Enumerable.Range(0, 4);
        return lineRange.Any(SpellsBingoHorizontal) 
            || lineRange.Any(SpellsBingoVertical);
    }

    public int GetUnmarkedSum()
    {
        return numbers.Except(marked).Sum();
    }

    bool SpellsBingoHorizontal(int line)
    {
        var numsOnLine = GetNumsOnLine(line * 5, 1);

        return ContainsAll(marked, numsOnLine);
    }

    bool SpellsBingoVertical(int line)
    {   
        var numsOnLine = GetNumsOnLine(line, 5);

        return ContainsAll(marked, numsOnLine);
    }

    bool ContainsAll(IEnumerable<int> items, IEnumerable<int> toCheck)
    {
        return !toCheck.Except(items).Any();
    }

    IEnumerable<int> GetNumsOnLine(int index, int step)
    {
        yield return numbers[index];
        yield return numbers[index + step];
        yield return numbers[index + 2 * step];
        yield return numbers[index + 3 * step];
        yield return numbers[index + 4 * step];
    }
}