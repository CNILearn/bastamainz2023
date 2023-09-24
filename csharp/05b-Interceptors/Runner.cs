namespace Interceptors;

public class Runner
{
    public void Bar()
    {
        DotheMagic(); // this method will be replaced
        DotheMagic(); // this method will do the Foo
    }

    public static void DotheMagic()
    {
        Console.WriteLine("Foo");
    }
}
