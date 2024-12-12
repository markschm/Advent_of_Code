#load "Utils.csx"
using System;
using System.Collections.Generic;

Dictionary<char, List<(int, int)>> BuildAntennaMap(string[] grid)
{
    var map = new Dictionary<char, List<(int, int)>>();
    for (int y = 0; y < grid.Length; y++)
    {
        for (int x = 0; x < grid[0].Length; x++)
        {
            if (grid[y][x] != '.')
            {
                if (!map.TryGetValue(grid[y][x], out List<(int, int)> coords))
                {
                    coords = new List<(int, int)>();
                    map[grid[y][x]] = coords;
                }

                coords.Add((x, y));
            }
        }
    }

    return map;
}

bool CoordExistsOnGrid((int, int) coord, int width, int height)
{
    var (x, y) = coord;
    if (x < 0 || x >= width || y < 0 || y >= height)
    {
        return false;
    }

    return true;
}

HashSet<(int, int)> FindAllNodesOnSlope((int, int) coords, int dx, int dy, int width, int height)
{
    var nodes = new HashSet<(int, int)>();
    nodes.Add(coords); // add current antenna point

    foreach (var (xStep, yStep) in new[] { (dx, dy), (-dx, -dy) })
    {
        var tempCoords = coords;
        while(CoordExistsOnGrid(
            tempCoords = (tempCoords.Item1 + xStep, tempCoords.Item2 + yStep), 
            width, 
            height))
        {
            nodes.Add(tempCoords);
        }
    }

    return nodes;
}


string[] grid = Utils.FileToString("day08").Split("\n");
int height = grid.Length;
int width = grid[0].Length;
var antinodeLocations = new HashSet<(int, int)>();
var antinodeLocations2 = new HashSet<(int, int)>();

var antennaLocationsMap = BuildAntennaMap(grid);

foreach (var entry in antennaLocationsMap)
{
    var coords = entry.Value;

    for (int i = 1; i < coords.Count; i++)
    {
        for (int n = 0; n < i; n++)
        {
            int dx = coords[i].Item1 - coords[n].Item1;
            int dy = coords[i].Item2 - coords[n].Item2;

            // part1
            var antinodeLocation = (coords[i].Item1 + dx, coords[i].Item2 + dy);
            if (CoordExistsOnGrid(antinodeLocation, width, height))
            {
                antinodeLocations.Add(antinodeLocation);
            }

            antinodeLocation = (coords[n].Item1 - dx, coords[n].Item2 - dy);
            if (CoordExistsOnGrid(antinodeLocation, width, height))
            {
                antinodeLocations.Add(antinodeLocation);
            }

            // part2
            antinodeLocations2.UnionWith(FindAllNodesOnSlope(coords[n], dx, dy, width, height));
        }
    }
}

Console.WriteLine($"Part 1: {antinodeLocations.Count()}");
Console.WriteLine($"Part 2: {antinodeLocations2.Count()}");
