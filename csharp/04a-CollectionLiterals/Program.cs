using System.Collections.Immutable;

using CollectionLiterals;

// use different collection types with collection expressiosn

Span<int> span1 = [1, 2, 3];
Console.WriteLine(span1[1]);

int[] arr = [5, 6];
List<int> list = [7, 8, 9];
HashSet<int> set = [10, 11, 12];
ImmutableArray<int> immArray = [13, 14, 15];

IList<int> iList = [16, 17, 18];
Console.WriteLine($"The type declared with IList<int> is {iList.GetType().Name}");

ICollection<int> iCollection = [19, 20, 21];

IReadOnlyList<int> iReadOnlyList = [20, 21, 22];

IReadOnlyCollection<int> iReadOnlyCollection = [23, 24, 25];

IEnumerable<int> iEnumerable = [26, 27, 28];

List<int> multiple = [..iList, 4, ..iCollection, ..span1[1..] ];

foreach (int i in multiple)
{
    Console.WriteLine(i);
}

// custom collections

MyCustomCollection<int> coll1 = [1, 2, 4, 8];
MyCustomCollection<int> coll2 = [7, 22, 33];
MyCustomCollection<int> coll3 = [.. coll1, .. coll2];
foreach (int item in coll3)
{
    Console.WriteLine(item);
}

// attention with spans!
byte[] buffer2 = new byte[4];
#pragma warning disable IDE0305 // Simplify collection initialization
// With the spread operator, a new collection is created. With the simplified collection initialization,
// the span references not the original collection
// https://github.com/dotnet/roslyn/issues/70101
// code analyzer issue is fixed with .NET 8 RC2
// TODO: remove this pragma with RC2
Span<byte> span2 = buffer2.AsSpan();
#pragma warning restore IDE0305 // Simplify collection initialization
// Span<byte> span2 = [.. buffer2];  // don'T use collection expressions here!
span2[0] = 1;
Console.WriteLine(buffer2[0]);
