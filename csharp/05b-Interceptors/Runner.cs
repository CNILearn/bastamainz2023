namespace Interceptors;

public class Runner
{
    public void Bar()
    {
        DotheMagic(42); // this method will be replaced
        DotheMagic(3); // this method will do the Foo
        Foo();
        Foo();
    }

    public static void DotheMagic(int x)
    {
        Console.WriteLine($"Foo with {x}");
    }

    public void Foo() 
    {
        Console.WriteLine("Foo");
    }
}
