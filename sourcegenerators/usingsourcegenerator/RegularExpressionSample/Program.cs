using System.Text.RegularExpressions;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run(typeof(TestIt));

// without benchmark
//TestIt testIt = new();
//testIt.TestWithReflection();
//testIt.TestWithGenerator();

//Console.WriteLine($"reg: {TestIt.s_matches_r} {TestIt.s_notMatches_r}");
//Console.WriteLine($"sc: {TestIt.s_matches_s} {TestIt.s_notMatches_s}");

public class TestIt
{
    internal static int s_matches_r = 0;
    internal static int s_notMatches_r = 0;
    internal static int s_matches_s = 0;
    internal static int s_notMatches_s = 0;

    [Benchmark]
    public void TestWithGenerator()
    {
        IsMatch("abc");
        IsMatch("abc");
        IsMatch("ABC");
        IsMatch("ghi");
        IsMatch("def");
    }

    [Benchmark]
    public void TestWithReflection()
    {
        IsMatchReflection("abc");
        IsMatchReflection("abc");
        IsMatchReflection("ABC");
        IsMatchReflection("ghi");
        IsMatchReflection("def");
    }

    static readonly Regex s_abcOrDef = new("abc|def", RegexOptions.IgnoreCase);

    static void IsMatchReflection(string text)
    {
        if (s_abcOrDef.IsMatch(text))
        {
            Interlocked.Increment(ref s_matches_r);
            // Console.WriteLine($"Match with {text}");
        }
        else
        {
            Interlocked.Increment(ref s_notMatches_r);
            // Console.WriteLine($"No match with {text}");
        }
    }

    static void IsMatch(string text)
    {
        if (MyRegex.AbcOrDefRegex().IsMatch(text))
        {
            Interlocked.Increment(ref s_matches_s);
            // Console.WriteLine($"Match with {text}");
        }
        else
        {
            Interlocked.Increment(ref s_notMatches_s);
            // Console.WriteLine($"No match with {text}");
        }
    }
}

public partial class MyRegex
{
    [GeneratedRegex("abc|def", RegexOptions.IgnoreCase)]
    public static partial Regex AbcOrDefRegex();
}