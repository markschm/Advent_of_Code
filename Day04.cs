#load "Utils.csx"
using System;


int SearchXMAS(string data)
{
    var directions = new[] {
        (0, 1), (0, -1), (1, 0), (-1, 0), (1, 1), (1, -1), (-1, 1), (-1, -1)
    };

    string[] grid = data.Split("\n");

    int count = 0;
    for (int y = 0; y < grid.Length; y++)
    {
        for (int x = 0; x < grid[0].Length; x++)
        {
            foreach (var (dx, dy) in directions)
            {
                count += CountPattern(grid, x, y, dx, dy, "XMAS");
            }
        }
    }

    return count;
}

int SearchFormattedMAS(string data)
{
    string[] grid = data.Split("\n");

    int count = 0;
    for (int y = 0; y < grid.Length; y++)
    {
        for (int x = 0; x < grid[0].Length; x++)
        {
            if (CountPattern(grid, x, y, 1, 1, "MAS") + CountPattern(grid, x, y, 1, 1, "SAM") > 0
             && CountPattern(grid, x, y + 2, 1, -1, "MAS") + CountPattern(grid, x, y + 2, 1, -1, "SAM") > 0)
            {
                count++;
            }
        }
    }

    return count;
}

int CountPattern(string[] grid, int x, int y, int dx, int dy, string pattern)
{
    for (int i = 0; i < pattern.Length; i++)
    {
        int cx = x + i * dx;
        int cy = y + i * dy;

        if (cx >= grid[0].Length || cx < 0 || cy >= grid.Length || cy < 0)
        {
            return 0;
        }

        if (grid[cy][cx] != pattern[i])
        {
            return 0;
        }
    }

    return 1;
}


string data = Utils.FileToString("day04");

int part1 = SearchXMAS(data);
int part2 = SearchFormattedMAS(data);

Console.WriteLine($"Part 1: {part1}");
Console.WriteLine($"Part 2: {part2}");
