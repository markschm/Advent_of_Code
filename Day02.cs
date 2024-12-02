#load "Utils.csx"
using System;


bool IsSafe(int[] levels, bool increasing)
{
    for (int i = 0; i < levels.Length - 1; i++)
    {
        int diff = increasing ? levels[i + 1] - levels[i] : levels[i] - levels[i + 1];
        if (1 > diff || diff > 3)
        {
            return false;
        }
    }

    return true;
}

bool IsSafeWDamper(int[] levels)
{
    for (int i = 0; i < levels.Length; i++)
    {
        int[] levelsWRemoved = levels.Where((_, index) => index != i).ToArray();
        if (IsSafe(levelsWRemoved, true) || IsSafe(levelsWRemoved, false)) 
        {
            return true;
        }
    }

    return false;
}


string data = Utils.FileToString("day02");

int safe = 0;
int safeWDamper = 0;
foreach (string line in data.Split("\n"))
{
    int[] levels = line
        .Split(" ", StringSplitOptions.RemoveEmptyEntries)
        .Select(level => int.Parse(level))
        .ToArray();

    // Part 1
    if (IsSafe(levels, levels[0] < levels[1]))
    {
        safe++;
    }

    // Part 2
    if (IsSafeWDamper(levels))
    {
        safeWDamper++;
    }
}

Console.WriteLine($"Part 1: {safe}");
Console.WriteLine($"Part 2: {safeWDamper}");
