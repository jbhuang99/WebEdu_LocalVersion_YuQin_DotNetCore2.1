using System;
using System.Globalization;

class Program
{
    static void Main(string[] args)
    {
        string op = "reverse";
        string input = null;

        foreach (var a in args)
        {
            if (a.StartsWith("--op=")) op = a.Substring(5);
            else if (a == "--help") { ShowHelp(); return; }
            else input = a;
        }

        if (string.IsNullOrEmpty(input))
        {
            Console.Write("Input: ");
            input = Console.ReadLine() ?? string.Empty;
        }

        string output = op switch
        {
            "reverse" => ReverseString(input),
            "upper" => input.ToUpperInvariant(),
            "lower" => input.ToLowerInvariant(),
            "trim" => input.Trim(),
            "base64" => Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(input)),
            "base64dec" => DecodeBase64(input),
            _ => $"Unknown op: {op}"
        };

        Console.WriteLine(output);
    }

    static string ReverseString(string s)
    {
        if (string.IsNullOrEmpty(s)) return s;
        var indexes = StringInfo.ParseCombiningCharacters(s);
        var parts = new string[indexes.Length];
        for (int i = 0; i < indexes.Length; i++)
        {
            int start = indexes[i];
            int length = (i + 1 < indexes.Length ? indexes[i + 1] : s.Length) - start;
            parts[i] = s.Substring(start, length);
        }
        Array.Reverse(parts);
        return string.Concat(parts);
    }

    static string DecodeBase64(string s)
    {
        try
        {
            var bytes = Convert.FromBase64String(s);
            return System.Text.Encoding.UTF8.GetString(bytes);
        }
        catch
        {
            return "Invalid base64 input";
        }
    }

    static void ShowHelp()
    {
        Console.WriteLine("Usage: dotnet run --file scripts-to-createSkillPlanNeededAdditionalTools/ProcessString.cs [--op=name] \"input string\"");
        Console.WriteLine("ops: reverse (default), upper, lower, trim, base64, base64dec");
    }
}
