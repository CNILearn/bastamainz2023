namespace System;

internal static class CommonExtensions
{
    public static string[] ToStrings(this IEnumerable<long> longs) =>
        longs.Select(x => x.ToString()).ToArray();
}
