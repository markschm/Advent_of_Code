#load "Utils.csx"
using System;
using System.Collections.Generic;

var DIRECTIONS = new (int, int)[] {
    (1, 0), (0, 1), (-1, 0), (0, -1)
};

List<(int, int)> FindStartingPoints(string[] grid)
{
    var points = new List<(int, int)>();

    for (int y = 0; y < grid.Length; y++)
    {
        for (int x = 0; x < grid[0].Length; x++)
        {
            if (grid[y][x] == '0')
            {
                points.Add((x, y));
            }
        }
    }

    return points;
}

int CalculateTrailScore(string[] grid, (int, int) startPoint)
{
    var endLocations = new HashSet<(int, int)>();
    var nextLocationsToSearch = new Queue<(int, int)>();
    nextLocationsToSearch.Enqueue(startPoint);

    // REMINDER: I was an idiot an left the grid as a string[]
    // so need to compare to number in single quotes
    while (nextLocationsToSearch.Count() > 0)
    {
        var (x, y) = nextLocationsToSearch.Dequeue();
        int currHeight = grid[y][x];

        if (currHeight == '9')
        {
            endLocations.Add((x, y));
            continue;
        }

        foreach (var (cx, cy) in DIRECTIONS)
        {
            int newX = x + cx;
            int newY = y + cy;
            if (Utils.ValidLocationInGrid(grid, newX, newY)
             && grid[newY][newX] - currHeight == 1)
            {
                nextLocationsToSearch.Enqueue((newX, newY));
            }
        }
    }

    return endLocations.Count();
}

int CalculateTrailRating(string[] grid, (int, int) point)
{
    var (x, y) = point;
    if (grid[y][x] == '9')
    {
        return 1;
    }

    int totalRating = 0;
    foreach (var (cx, cy) in DIRECTIONS)
    {
        int newX = x + cx;
        int newY = y + cy;

        if (Utils.ValidLocationInGrid(grid, newX, newY)
         && grid[newY][newX] - grid[y][x] == 1)
        {
            totalRating += CalculateTrailRating(grid, (newX, newY));
        }
    }

    return totalRating;
}


string[] grid = Utils.FileToString("day10").Split("\n");
var startingPoints = FindStartingPoints(grid);

int part1 = 0;
int part2 = 0;
foreach (var point in startingPoints)
{
    part1 += CalculateTrailScore(grid, point);
    part2 += CalculateTrailRating(grid, point);
}

Console.WriteLine($"Part 1: {part1}");
Console.WriteLine($"Part 2: {part2}");
