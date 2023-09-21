using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;

namespace BlazorWasmSample.Client;

[SupportedOSPlatform("browser")]
public partial class Interop
{
    [JSExport]
    public static int Add(int x, int y) => x + y;
    [JSExport]
    public static int Subtract(int x, int y) => x - y;

    [JSImport("addJS", "Interop")]
    public static partial int AddJS(int x, int y);

    [JSImport("subtractJS", "Interop")]
    public static partial int SubtractJS(int x, int y);
}
