namespace System;

internal static class ParseExtensions
{
    public static IEnumerable<long> ParseLongsSafely(this string[] strings)
    {
        foreach (var s in strings)
            if (long.TryParse(s, out var value))
                yield return value;
    }
}
