#load "Utils.csx"
using System;
using System.Collections.Generic;


Dictionary<int, HashSet<int>> BuildMapFromRules(string rules)
{
    var ruleMap = new Dictionary<int, HashSet<int>>();

    foreach (string line in rules.Split("\n"))
    {
        var (key, value) = Utils.SplitInTwoAndConvert("|", line, int.Parse);

        if (!ruleMap.TryGetValue(key, out HashSet<int> hs))
        {
            hs = new HashSet<int>();
            ruleMap[key] = hs;
        }

        hs.Add(value);
    }

    return ruleMap;
}

void DisplayMap(Dictionary<int, HashSet<int>> map)
{
    foreach(var kvp in ruleMap)
    {
        Console.WriteLine($"Key: {kvp.Key}, Values: {string.Join(", ", kvp.Value)}");
    }
}

(int, int)? PageIndexesToSwap(Dictionary<int, HashSet<int>> ruleMap, int[] pages)
{
    for (int i = 1; i < pages.Length; i++)
    {
        if (ruleMap.TryGetValue(pages[i], out HashSet<int> hs))
        {
            for (int n = 0; n < i; n++)
            {
                if (hs.Contains(pages[n]))
                {
                    return (n, i);
                }
            }
        }
    }

    return null;
}

void FixOrder(Dictionary<int, HashSet<int>> ruleMap, int[] pages)
{
    while (true)
    {
        var swapIndexes = PageIndexesToSwap(ruleMap, pages);
        if (!swapIndexes.HasValue)
        {
            break;
        }

        var (i1, i2) = swapIndexes.Value;
        Utils.Swap(ref pages[i1], ref pages[i2]);
    }
}


string[] splitData = Utils.FileToString("day05").Split("\n\n");
string rules = splitData[0];
string pageOrdering = splitData[1];

var ruleMap = BuildMapFromRules(rules);
// DisplayMap(ruleMap);

int part1 = 0;
int part2 = 0;
foreach (string pageOrder in pageOrdering.Split("\n"))
{
    int[] pages = pageOrder.Split(",").Select(num => int.Parse(num)).ToArray();
    int midIndex = pages.Length / 2;    
    
    if (PageIndexesToSwap(ruleMap, pages) == null)
    {
        part1 += pages[midIndex];
    }
    else
    {
        FixOrder(ruleMap, pages);
        part2 += pages[midIndex];
    }
}

Console.WriteLine($"Part 1: {part1}");
Console.WriteLine($"Part 2: {part2}");
