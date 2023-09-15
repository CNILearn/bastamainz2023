using System.Text.RegularExpressions;

// Regex abcOrDev = new("abc|def", RegexOptions.Compiled | RegexOptions.IgnoreCase);

IsMatch("abc");
IsMatch("ABC");
IsMatch("ghi");
IsMatch("def");

static void IsMatch(string text)
{
    if (MyRegex.AbcOrDefRegex().IsMatch(text))
    {
        Console.WriteLine($"Match with {text}");
    }
    else
    {
        Console.WriteLine($"No match with {text}");
    }
}

public partial class MyRegex
{
    [GeneratedRegex("abc|def", RegexOptions.IgnoreCase)]
    public static partial Regex AbcOrDefRegex();
}