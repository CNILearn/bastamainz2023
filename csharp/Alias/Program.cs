using GameStart = (string Name, string GameType);
using OneS = One<string>;
using OneI = One<int>;

GameStart s = new("Christian", "Game6x4");

(string name, _) = s;

Console.WriteLine(name);
Console.WriteLine(s.GameType);

OneS os = new();
os.Foo("a string");

OneI oi = new();
oi.Foo(42);

public class One<T>
{
    public void Foo(T item)
    {
        Console.WriteLine(item);
    }
}

