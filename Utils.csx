using System;
using System.IO;

public static class Utils 
{
    public static string FileToString(string filename) 
    {
        using (StreamReader streamReader = new StreamReader("input/" + filename))
        {
            return streamReader.ReadToEnd();
        }
    }

    public static (T, T) SplitInTwoAndConvert<T>(
        string sep, string input, Func<string, T> converter)
    {
        var splitInput = input.Split(sep, StringSplitOptions.RemoveEmptyEntries)
            .Select(converter)
            .ToArray();

        return (splitInput[0], splitInput[1]);
    }

    public static void Swap<T>(ref T a, ref T b)
    {
        T temp = a;
        a = b;
        b = temp;
    }

    public static (int, int) AddPair((int, int) a, (int, int) b) 
        => (a.Item1 + b.Item1, a.Item2 + b.Item2);
}
