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
}
