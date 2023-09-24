using GameStart = (string Name, string GameType);
using OneS = One<string>;
using OneI = One<int>;
using IntArray = int[];
using unsafe MyPtr = int*;

GameStart s = new("Christian", "Game6x4");

(string name, _) = s;

Console.WriteLine(name);
Console.WriteLine(s.GameType);

OneS os = new();
os.Foo("a string");

OneI oi = new();
oi.Foo(42);

IntArray ia = [ 2, 3, 4 ];
ia[0] = 42;
ia[1] = 43;

Foo(ia[1]);

unsafe void Foo(int x)
{
    MyPtr pt = &x;
    Console.WriteLine(*pt);
}

public class One<T>
{
    public void Foo(T item)
    {
        Console.WriteLine(item);
    }
}

