using System;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;

Console.WriteLine("Hello, Console!");

return 0;

[SupportedOSPlatform("browser")]
public partial class MyClass
{
    [JSExport]
    internal static string Greeting()
    {
        var text = $"Hello, World! Greetings from node version: {GetNodeVersion()}";
        return text;
    }

    [JSImport("node.process.version", "main.mjs")]
    internal static partial string GetNodeVersion();

    [JSExport]
    internal static int Add(int x, int y) => x + y;
}
