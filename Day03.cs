#load "Utils.csx"
using System;
using System.Text.RegularExpressions;


int mul(Match match) 
{
    return match.Value
        .Substring(4, match.Length - 5)
        .Split(",")
        .Select(num => int.Parse(num))
        .Aggregate(1, (acc, num) => acc * num);
}

string data = Utils.FileToString("day03");

string pattern = @"mul\(\d{1,3},\d{1,3}\)";
string pattern2 = pattern + @"|do\(\)|don't\(\)";

int part1 = Regex.Matches(data, pattern).Select(mul).Sum();

bool enabled = true;
int part2 = Regex.Matches(data, pattern2)
    .Aggregate(0, (sum, match) => {
        string val = match.Value;

        if (val == "do()")
        {
            enabled = true;
        }
        else if (val == "don't()")
        {
            enabled = false;
        }
        else if (enabled)
        {
            sum += mul(match);
        }

        return sum;
    });


Console.WriteLine($"Part 1: {part1}");
Console.WriteLine($"Part 2: {part2}");
