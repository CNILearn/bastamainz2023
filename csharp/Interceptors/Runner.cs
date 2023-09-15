namespace Interceptors;

public class Runner
{
    public void Bar()
    {
        DotheMagic();
        //Foo();
    }

    public static void DotheMagic()
    {
        Console.WriteLine("Foo");
    }
}
