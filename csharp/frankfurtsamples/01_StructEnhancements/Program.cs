// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

MyRecord1 r1 = new();

MyRecord2 r2 = new();

MyData mydata1 = new();
MyData mydata2 = mydata1 with { Z = 42 };
Console.WriteLine(r1);
Console.WriteLine(r2);
Console.WriteLine(mydata1);
Console.WriteLine(mydata2);

// positional records
public readonly record struct MyRecord1(int X, int Y);

public record struct MyRecord2(int X, int Y);

public struct MyData
{
    public MyData()
    {
        X = -1;
        Y = -1;
    }

    public int X;
    public int Y;
    public int Z;

    public override string ToString() => $"X: {X}, Y: {Y}, Z: {Z}";

}
