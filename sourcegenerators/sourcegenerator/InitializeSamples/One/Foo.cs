using CNinnovation.Samples;

namespace One;

public class Foo : IInitialize
{
    public void Init()
    {
        Console.WriteLine($"{nameof(Foo)}.{nameof(Init)}");
    }
}