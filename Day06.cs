#load "Utils.csx"
using System;
using System.Collections.Generic;
using System.Text;

(int, int) GetStartLocation(string[] grid)
{
    for (int y = 0; y < grid.Length; y++)
    {
        for (int x = 0; x < grid[0].Length; x++)
        {
            if (grid[y][x] == '^')
            {
                return (x, y);
            }
        }
    }

    throw new Exception("Couldn't find starting location.");
}

bool IsValidLocation((int, int) location, string[] grid)
{
    return 0 <= location.Item1 && location.Item1 < grid[0].Length 
        && 0 <= location.Item2 && location.Item2 < grid.Length;
}

(int, int) TurnRight90((int, int) dir)
{
    if ((1, 0) == dir) { return (0, 1); } 
    if ((0, 1) == dir) { return (-1, 0); }
    if ((-1, 0) == dir) { return (0, -1); }
    return (1, 0);
}

int CountUniqueTraversalLocations(string[] grid, int part = 1)
{
    HashSet<(int, int)> locations = new HashSet<(int, int)>();
    int counter = 0;
    int lastLocationCount = 0;
    var dir = (0, -1);
    var location = GetStartLocation(grid);

    while (IsValidLocation(location, grid))
    {
        locations.Add(location);

        var newLocation = Utils.AddPair(location, dir);
        while (IsValidLocation(newLocation, grid) 
         && grid[newLocation.Item2][newLocation.Item1] == '#')
        {
            dir = TurnRight90(dir);
            newLocation = Utils.AddPair(location, dir);
        }

        location = newLocation;

        // only used for part2
        if (part == 2 && counter++ > 100)
        {
            counter = 0;
            if (lastLocationCount == locations.Count)
            {
                return -1;
            }

            lastLocationCount = locations.Count;
        }
    }

    return locations.Count;
}

int TestAllObstructions(string[] grid)
{
    int trappedCount = 0;
    for (int y = 0; y < grid.Length; y++)
    {
        for (int x = 0; x < grid[0].Length; x++)
        {
            if (grid[y][x] == '^' || grid[y][x] == '#')
            {
                continue;
            }

            // realizing here I fucked up keeping string[] instead of using char[][] 
            // ... but too late now don't feel like going back
            string temp = grid[y];
            StringBuilder sb = new StringBuilder(grid[y]);
            sb[x] = '#';
            grid[y] = sb.ToString();
            if (CountUniqueTraversalLocations(grid, 2) == -1)
            {
                trappedCount++;
            }
            grid[y] = temp;
        }
    }

    return trappedCount;
}


string[] grid = Utils.FileToString("day06").Split("\n");

int part1 = CountUniqueTraversalLocations(grid);
int part2 = TestAllObstructions(grid);

Console.WriteLine($"Part 1: {part1}");
Console.WriteLine($"Part 2: {part2}");
