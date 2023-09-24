Point p1 = new Point(3, 4);
Point2 p2 = new Point2(3, 4);

MyStruct ms1 = new(1, 2, 3);
ms1.Foo();


// properties with get and init accessor
public record class Book(string Title, string? Publisher, DateOnly ReleaseDate);

// properties with get and set accessor
public record struct Point(double X, double Y);

// properties with get and init accessor
public readonly record struct Point2(double X, double Y);

// Primary constructor
public struct MyStruct(int x, int y, int z)
{
    private int _y = y;
    public int X { get; init; } = x;
    public void Foo()
    {
        Console.WriteLine($"X: {X}, y: {_y}, z: {z}");
    }
}