using CNinnovation.Samples;

using System.Reflection;

[assembly: CNinnovation.Samples.Initialize]

Initialize.Init();

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

public class FooBar : IInitialize
{
    public void Init()
    {
        Console.WriteLine("FooBar.init");
    }
}
