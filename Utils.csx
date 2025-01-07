using System;
using System.IO;
using System.Collections.Generic;

// class won't be organized it's just a bunch of functions that I thought
// i'd use more than once during these challenges so I just tossed them in here
public static class Utils 
{
    // constants
    public static readonly (int, int)[] DIRECTIONS = new (int, int)[] {
        (1, 0), (0, 1), (-1, 0), (0, -1)
    };

    public static readonly (int, int)[] CORNER_DIRECTIONS = new (int, int)[] {
        (1, 1), (-1, 1), (1, -1), (-1, -1)
    };


    public static string FileToString(string filename) 
    {
        using (StreamReader streamReader = new StreamReader("input/" + filename))
        {
            return streamReader.ReadToEnd();
        }
    }

    public static T[] SplitAndConvert<T>(string sep, string input, Func<string, T> converter)
        => input.Split(sep, StringSplitOptions.RemoveEmptyEntries)
            .Select(converter)
            .ToArray();

    public static (T, T) SplitInTwoAndConvert<T>(
        string sep, string input, Func<string, T> converter)
    {
        var res = SplitAndConvert(sep, input, converter);
        return (res[0], res[1]);
    }

    public static void Swap<T>(ref T a, ref T b)
    {
        T temp = a;
        a = b;
        b = temp;
    }

    public static (int, int) AddPair((int, int) a, (int, int) b) 
        => (a.Item1 + b.Item1, a.Item2 + b.Item2);

    public static int CountSymbolInGrid(char[][] grid, char symbol) 
        => grid.Select(line => line.Where(ch => ch == symbol).Count()).Sum();

    public static char[][] BuildGrid(int width, int height, char defaultVal = ' ')
        => Enumerable.Range(0, height)
            .Select(_ => Enumerable.Repeat(defaultVal, width).ToArray())
            .ToArray();

    public static bool ValidLocationInGrid(string[] grid, int x, int y)
    {
        return 0 <= y && y < grid.Length && 0 <= x && x < grid[0].Length;
    }
    public static bool ValidLocationInGrid(string[] grid, (int, int) location)
    {
        var (x, y) = location;
        return ValidLocationInGrid(grid, x, y);
    }

    public static (int, int) FindLocationInGrid(char[][] grid, char c)
    {
        for (int y = 0; y < grid.Length; y++)
        {
            for (int x = 0; x < grid[0].Length; x++)
            {
                if (grid[y][x] == c)
                {
                    return (x, y);
                }
            }
        }

        return (-1, -1);
    }

    public static void SwapInGrid(char[][] grid, (int, int) a, (int, int) b)
    {
        var (aX, aY) = a;
        var (bX, bY) = b;

        char temp = grid[aY][aX];
        grid[aY][aX] = grid[bY][bX];
        grid[bY][bX] = temp; 
    }


    // Display Functions
    public static void PrintGrid(char[][] grid) => Array.ForEach(grid, Console.WriteLine);
    public static void PrintGrid(string[] grid) => Array.ForEach(grid, Console.WriteLine);
    
    public static void PrintMap<K, V>(Dictionary<K, V> map, Func<V, string> converter)
    {
        foreach (var entry in map)
        {
            Console.WriteLine($"{entry.Key}: {converter(entry.Value)}");
        }
    }
    public static void PrintMap<K, V>(Dictionary<K, V> map)
    {
        PrintMap(map, (val => val?.ToString() ?? "NO STRING REPRESENTATION"));
    }

    public static void PrintList<T>(List<T> list)
        => Console.WriteLine(string.Join(", ", list));
}
