#load "Utils.csx"
using System;
using System.Text;

List<int> FormatDiskToBlocks(string data)
{
    int id = 0;

    var diskBlocks = new List<int>();
    for (int i = 0; i < data.Length; i ++)
    {
        for (int n = 0; n < data[i] - '0'; n++)
        {
            diskBlocks.Add(i % 2 == 0 ? id : -1);
        }

        id += i % 2 == 0 ? 1 : 0;
    }

    return diskBlocks;
}

void CompactDiskFragmentation(List<int> diskBlocks)
{
    for (int i = 0; i < diskBlocks.Count(); i++)
    {
        if (diskBlocks[i] == -1)
        {
            int end = diskBlocks.Count() - 1;
            while (diskBlocks[end] == -1)
            {
                diskBlocks.RemoveAt(end--);
            }

            diskBlocks[i] = diskBlocks[end];
            diskBlocks.RemoveAt(end);
        }
    }
}

int FindFreeSpaceOfBlockSize(List<int> diskBlocks, int blockSize, int rPointer)
{
    for (int i = 0; i < rPointer - blockSize; i++)
    {
        if (diskBlocks[i] == -1)
        {
            int offset = 0;
            while (offset < blockSize)
            {
                if (diskBlocks[i + offset] != -1)
                {
                    break;
                }
                
                if (++offset == blockSize)
                {
                    return i;
                }
            }
        }
    }

    return -1;
}

void CompactDiskNoFragmentation(List<int> diskBlocks)
{
    int rPointer = diskBlocks.Count() - 1;
    int blockSize = 1;

    while (rPointer >= 0)
    {
        while (diskBlocks[rPointer] == -1)
        {
            rPointer--;
        }

        blockSize = 0;
        while (rPointer - blockSize >= 0 && diskBlocks[rPointer - blockSize] == diskBlocks[rPointer])
        {
            blockSize++;
        }

        int freeSpaceStartIndex = FindFreeSpaceOfBlockSize(diskBlocks, blockSize, rPointer);
        if (freeSpaceStartIndex != -1)
        {
            for (int i = 0; i < blockSize; i++)
            {
                int temp = diskBlocks[rPointer - i];
                diskBlocks[freeSpaceStartIndex + i] = temp;
                diskBlocks[rPointer - i] = -1;
            }
        }

        rPointer -= blockSize;
    }
}

string data = Utils.FileToString("day09");
var diskBlocks = FormatDiskToBlocks(data);
var diskBlocks2 = diskBlocks.Select(element => element).ToList();

CompactDiskFragmentation(diskBlocks);
long part1 = diskBlocks.Select((element, index) => (long)(element * index)).Sum();

Utils.PrintList(diskBlocks2);
CompactDiskNoFragmentation(diskBlocks2);
Utils.PrintList(diskBlocks2);

long part2 = diskBlocks2
    .Select((element, index) => element != -1 ? (long)(element * index) : 0)
    .Sum();

Console.WriteLine($"Part 1: {part1}");
Console.WriteLine($"Part 2: {part2}");
