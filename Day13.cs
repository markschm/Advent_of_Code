#load "Utils.csx"
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;


var data = Utils.FileToString("day13").Split("\n\n")
    .Select(group => {
        var coords = group.Split("\n")
            .Select(line => {
                var matches = Regex.Matches(line, @"\b\d+\b")
                    .Select(match => int.Parse(match.Value))
                    .ToArray();

                return (X: matches[0], Y: matches[1]);
            })
            .ToArray();

        return (coords[0], coords[1], coords[2]);
    });

int part1 = 0;
int part2 = 0;
foreach (var (a, b, prize) in data)
{
    // (# of A, # of B)
    var visited = new HashSet<(int, int)>();
    var toVisit = new Queue<(int, int)>();
    toVisit.Enqueue((0, 0));

    while (toVisit.Count() > 0)
    {
        var (aCount, bCount) = toVisit.Dequeue();

        int currX = a.X * aCount + b.X * bCount;
        int currY = a.Y * aCount + b.Y * bCount;
        
        if (currX == prize.X && currY == prize.Y)
        {
            // A button = 3 tokens, B button = 1 token
            part1 += aCount * 3 + bCount;
            break;
        }

        // check and add next combinations to toVisit
        if (currX + a.X <= prize.X && currY + a.Y <= prize.Y
         && !visited.Contains((aCount + 1, bCount)))
        {
            toVisit.Enqueue((aCount + 1, bCount));
            visited.Add((aCount + 1, bCount));
        }

        if (currX + b.X <= prize.X && currY + b.Y <= prize.Y
         && !visited.Contains((aCount, bCount + 1)))
        {
            toVisit.Enqueue((aCount, bCount + 1));
            visited.Add((aCount, bCount + 1));
        }
    }
}

Console.WriteLine($"Part 1: {part1}");
Console.WriteLine($"Part 2: {part2}");
