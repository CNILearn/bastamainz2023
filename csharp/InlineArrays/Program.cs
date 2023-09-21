using System.Runtime.CompilerServices;

Buffer b1 = new();
for (int i = 0; i < 10; i++)
{
    b1[i] = i;
}

for (int i = 0; i < 10; i++)
{
    Console.WriteLine(i);
}

Test2(b1);
Test1(b1);

//unsafe void Foo(Buffer b)
//{
//    long* l = stackalloc long[10];
//    Console.WriteLine();
//}

void Test2(Span<int> s1)
{
    for (int i = 0; i < s1.Length; i++)
    {
        s1[i] = s1[i] + 2;
    }
}

void Test1(Span<int> s1)
{
    foreach (int item in s1)
    {
        Console.WriteLine(item);
    }
}

[InlineArray(10)]
public struct Buffer
{
    private int _x;
}
