#load "Utils.csx"
using System;

string data = Utils.FileToString("day11");
var stoneMap = Utils.SplitAndConvert(" ", data, long.Parse)
    .GroupBy(x => x)
    .ToDictionary(g => g.Key, g => (long) g.Count());

long part1 = 0;
long part2 = 0;

for (int i = 0; i < 75; i++)
{
    if (i == 25)
    {
        part1 = stoneMap.Values.Sum();
    }

    var newStones = new Dictionary<long, long>();
    foreach (var element in stoneMap)
    {
        long stone = element.Key;
        long stoneCount = element.Value;

        if (stone == 0)
        {
            if (!newStones.ContainsKey(1))
            {
                newStones[1] = 0;
            }
            newStones[1] += stoneCount;
        }
        else if (stone.ToString().Length % 2 == 0) 
        {
            string stoneStr = stone.ToString();
            int halfLength = stoneStr.Length / 2;

            long left = long.Parse(stoneStr[..halfLength]);
            if (!newStones.ContainsKey(left))
            {
                newStones[left] = 0;
            }
            newStones[left] += stoneCount;

            long right = long.Parse(stoneStr[halfLength..]);
            if (!newStones.ContainsKey(right))
            {
                newStones[right] = 0;
            }
            newStones[right] += stoneCount;
        }
        else 
        {
            long newStoneVal = stone * 2024;
            if (!newStones.ContainsKey(newStoneVal))
            {
                newStones[newStoneVal] = 0;
            }
            newStones[newStoneVal] += stoneCount;
        }
    }

    stoneMap = newStones;
}
part2 = stoneMap.Values.Sum();

Console.WriteLine($"Part 1: {part1}");
Console.WriteLine($"Part 2: {part2}");
