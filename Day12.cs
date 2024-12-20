#load "Utils.csx"
using System;
using System.Collections.Generic;


string[] grid = Utils.FileToString("day12").Split("\n");

int part1 = 0;
int part2 = 0;

var visited = new HashSet<(int, int)>();
for (int y = 0; y < grid.Length; y++)
{
    for (int x = 0; x < grid[0].Length; x++)
    {
        if (visited.Contains((x, y)))
        {
            continue;
        }

        char currPlant = grid[y][x];
        var toVisit = new Queue<(int, int)>();
        toVisit.Enqueue((x, y));

        int area = 0;
        int perimeter = 0;
        int corners = 0;
        while (toVisit.Count() > 0)
        {
            var (cx, cy) = toVisit.Dequeue();

            if (Utils.ValidLocationInGrid(grid, cx, cy) 
             && grid[cy][cx] == currPlant)
            {
                if (visited.Contains((cx, cy)))
                {
                    continue;
                }

                visited.Add((cx, cy));

                foreach (var (dx, dy) in Utils.DIRECTIONS)
                {
                    toVisit.Enqueue((cx + dx, cy + dy));
                }


                area++;
            }
            else
            {
                // part 1
                perimeter++;
            }


            // part 2
            if (Utils.ValidLocationInGrid(grid, cx, cy) && grid[cy][cx] == currPlant)
            {
                foreach (var (dx, dy) in Utils.CORNER_DIRECTIONS)
                {
                    // outside corners
                    if ((!Utils.ValidLocationInGrid(grid, cx + dx, cy) || grid[cy][cx + dx] != currPlant) &&
                        (!Utils.ValidLocationInGrid(grid, cx, cy + dy) || grid[cy + dy][cx] != currPlant))
                    {
                        corners++;
                    }
                    
                    // inside corners
                    if (Utils.ValidLocationInGrid(grid, cx + dx, cy) && grid[cy][cx + dx] == currPlant &&
                        Utils.ValidLocationInGrid(grid, cx, cy + dy) && grid[cy + dy][cx] == currPlant && 
                        grid[cy + dy][cx + dx] != currPlant)
                    {
                        corners++;
                    }
                }
            }
        }

        part1 += area * perimeter;
        part2 += area * corners;
    }
}

Console.WriteLine($"Part 1: {part1}");
Console.WriteLine($"Part 2: {part2}");
