#load "Utils.csx"
using System;


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
