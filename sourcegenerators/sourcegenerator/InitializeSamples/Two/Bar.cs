using CNinnovation.Samples;

namespace Two;

public class Bar : IInitialize
{
    public void Init()
    {
        Console.WriteLine($"{nameof(Bar)}.{nameof(Init)}");
    }
}