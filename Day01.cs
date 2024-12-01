#load "Utils.csx"
using System;
using System.Linq;


string data = Utils.FileToString("day01");

string[] lines = data.Split("\n");
int fileSize = lines.Length;

int[] left = new int[fileSize];
int[] right = new int[fileSize];

for (int i = 0; i < fileSize; i++) 
{
    string[] nums = lines[i].Split(" ", StringSplitOptions.RemoveEmptyEntries);
    
    left[i] = int.Parse(nums[0]);
    right[i] = int.Parse(nums[1]);    
}

Array.Sort(left);
Array.Sort(right);

int totalDiff = 0;
int similarityScore = 0;
for (int i = 0; i < fileSize; i++) 
{
    // Part 1
    totalDiff += Math.Abs(left[i] - right[i]);

    // Part 2
    int appearancesInRight = 0; 
    for (int n = 0; n < fileSize; n++) 
    {
        if (right[n] == left[i])
        {
            appearancesInRight++;
        }
    }

    similarityScore += left[i] * appearancesInRight;
}

Console.WriteLine($"Part 1: {totalDiff}");
Console.WriteLine($"Part 2: {similarityScore}");


// ////////////////////////////////////////////////////////////////////////////
// Trying to learn C# while doing this so another way w/ Linq

// var parsedData = data.Split("\n")
//     .Select(line => line.Split(" ", StringSplitOptions.RemoveEmptyEntries))
//     .Select(nums => (Left: int.Parse(nums[0]), Right: int.Parse(nums[1])))
//     .ToArray();

// int[] left = parsedData.Select(pair => pair.Left).OrderBy(x => x).ToArray();
// int[] right = parsedData.Select(pair => pair.Right).OrderBy(x => x).ToArray();

// int totalDiff = left.Zip(right, (l, r) => Math.Abs(l - r)).Sum();

// var freqMap = right.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
// int similarityScore = left
//     .Where(num => freqMap.ContainsKey(num))
//     .Select(num => num * freqMap[num])
//     .Sum();

// Console.WriteLine($"Part 1: {totalDiff}");
// Console.WriteLine($"Part 2: {similarityScore}");
