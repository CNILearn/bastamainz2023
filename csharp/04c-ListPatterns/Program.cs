using System.Numerics;

int[] list1 = [1, 2, 3, 4, 5, 6, 7, 8, 9];
double[] list2 = [1, 2, 3, 4.4, 5, 6, 7, 8, 9];

int result = AddAll1(list1);
Console.WriteLine(result);

double result2 = AddAll2(list2);
Console.WriteLine(result2);

double result3 = AddAll3(list2);
Console.WriteLine(result3);

double result4 = AddAll4(list2.AsSpan());
Console.WriteLine(result4);

int AddAll1(int[] values)
{
    int result = 0;
    foreach (int value in values)
    {
        result += value;
    }
    return result;
}

// with INumberBase<T> constraint!
T AddAll2<T>(T[] values) where T : INumberBase<T>
{
    T result = T.Zero;
    foreach (T value in values)
    {
        result += value;
    }
    return result;
}

// with list pattern matching - and INumberBase<T> constraint
T AddAll3<T>(T[] values) where T : INumberBase<T> =>
    values switch
    {
        [] => T.Zero,
        [var first, .. var rest] => first + AddAll3(rest),
    };

T AddAll4<T>(Span<T> values) where T : INumberBase<T> =>
    values switch
    {
        [] => T.Zero,
        [var first, .. var rest] => first + AddAll4(rest),
    };
