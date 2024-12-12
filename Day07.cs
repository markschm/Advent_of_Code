#load "Utils.csx"
using System;

enum Operator { PLUS, MULTIPLY, CONCAT }

long EvalWithOperator(long a, long b, Operator op)
{
    if (op == Operator.PLUS)
    {
        return a + b;
    }
    else if (op == Operator.CONCAT)
    {
        return long.Parse($"{a}{b}");
    }
    return a * b;
}

bool ProducesTargetValue(long target, long val, ReadOnlySpan<long> nums, Operator op)
{
    val = EvalWithOperator(val, nums[0], op);

    if (nums.Length == 1)
    {
        return val == target;
    }

    ReadOnlySpan<long> newNums = nums.Slice(1);
    return ProducesTargetValue(target, val, newNums, Operator.PLUS) 
        || ProducesTargetValue(target, val, newNums, Operator.MULTIPLY);
}

// shitty way of doing it having both these functions when just one line is different
// but i'm trying to catch up on missed days so just going to leave this cuz it works
bool ProducesTargetValue2(long target, long val, ReadOnlySpan<long> nums, Operator op)
{
    val = EvalWithOperator(val, nums[0], op);

    if (nums.Length == 1)
    {
        return val == target;
    }

    ReadOnlySpan<long> newNums = nums.Slice(1);
    return ProducesTargetValue2(target, val, newNums, Operator.PLUS) 
        || ProducesTargetValue2(target, val, newNums, Operator.MULTIPLY)
        || ProducesTargetValue2(target, val, newNums, Operator.CONCAT);
}


string data = Utils.FileToString("day07");

long part1 = 0;
long part2 = 0;
foreach (string line in data.Split("\n"))
{
    string[] splitOnColon = line.Split(":");
    long target = long.Parse(splitOnColon[0]);
    long[] nums = Utils.SplitAndConvert(" ", splitOnColon[1], long.Parse);

    if (ProducesTargetValue(target, 0, nums.AsSpan(), Operator.PLUS))
    {
        part1 += target;
    }
    
    if (ProducesTargetValue2(target, 0, nums.AsSpan(), Operator.PLUS))
    {
        part2 += target;
    }
}

Console.WriteLine($"Part 1: {part1}");
Console.WriteLine($"Part 2: {part2}");
