using System.Runtime.CompilerServices;

// create a fixed size buffer
Buffer<int> b2 = new();
b2[0] = 3;

// Create a span to reference the fixed size buffer and fill it
Span<int> span2 = b2;
span2.Fill(42);

// Create an instance of the Buffer<T> type with Person objects
Buffer<Person> b3 = new();
b3[0] = new Person { First = "John", Last = "Doe" };
Person p1 = b3[0];
p1.First = "Jane";
Console.WriteLine(b3[0].First);

Buffer b1 = new();
for (int i = 0; i < 10; i++)
{
    b1[i] = i;
}

Console.WriteLine("Show inline array");
for (int i = 0; i < 10; i++)
{
    Console.Write($"{i} ");
}
Console.WriteLine();
Console.WriteLine();

ChangeInlineArray(b1);
ShowInlineArray(b1);

void ChangeInlineArray(Span<int> s1)
{
    for (int i = 0; i < s1.Length; i++)
    {
        s1[i] = s1[i] + 2;
    }
}

void ShowInlineArray(Span<int> s1)
{
    Console.WriteLine(nameof(ShowInlineArray));
    foreach (int item in s1)
    {
        Console.Write($"{item} ");
    }
    Console.WriteLine();
}

[InlineArray(10)]
public struct Buffer
{
#pragma warning disable IDE0044 // Add readonly modifier
#pragma warning disable IDE0051 // Remove unused private members
    private int _x;
#pragma warning restore IDE0051 // Remove unused private members
#pragma warning restore IDE0044 // Add readonly modifier
}

// generic buffer
[InlineArray(10)]
public struct Buffer<T>
{
#pragma warning disable IDE0044 // Add readonly modifier
#pragma warning disable IDE0051 // Remove unused private members
    private T _item;
#pragma warning restore IDE0051 // Remove unused private members
#pragma warning restore IDE0044 // Add readonly modifier
}

public class Person
{
    public string? First { get; set; }
    public string? Last { get; set; }
}