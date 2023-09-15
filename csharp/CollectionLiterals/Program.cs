using System.Collections.Immutable;

Span<int> onStack = [1, 2, 3];
Console.WriteLine(onStack[1]);

var value = new object();
Span<object> data = [value];

Console.WriteLine(data[0]);

int[] onHeap = [5, 6];
List<int> list = [7, 8, 9];
HashSet<int> set = [10, 11, 12];
ImmutableArray<int> immArray = [13, 14, 15];

IList<int> iList = [16, 17, 18];
Console.WriteLine(iList.GetType().Name);
ICollection<int> iCollection = [19, 20, 21];
Console.WriteLine(iCollection.GetType().Name);
IReadOnlyList<int> iReadOnlyList = [20, 21, 22];
Console.WriteLine(iReadOnlyList.GetType().Name);
IReadOnlyCollection<int> iReadOnlyCollection = [23, 24, 25];
Console.WriteLine(iReadOnlyCollection.GetType().Name);
IEnumerable<int> iEnumerable = [26, 27, 28];
Console.WriteLine(iEnumerable.GetType().Name);

List<int> multiple = [..iList, 4, ..iCollection, ..onStack[1..] ];


foreach (int i in multiple)
{
    Console.WriteLine(i);
}