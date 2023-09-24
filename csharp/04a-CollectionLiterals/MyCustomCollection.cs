using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace CollectionLiterals;

[CollectionBuilder(typeof(MyCustomCollection), nameof(MyCustomCollection.Create))]
internal class MyCustomCollection<T> : Collection<T>
{
}

internal static class MyCustomCollection
{
    public static MyCustomCollection<T> Create<T>(ReadOnlySpan<T> items)
    {
#pragma warning disable IDE0028 // Simplify collection initialization
 // results in recursive invocations https://github.com/dotnet/roslyn/issues/70099
        MyCustomCollection<T> collection = new();
#pragma warning restore IDE0028 // Simplify collection initialization
        foreach (T item in items)
        {
            collection.Add(item);
        }

        return collection;
    }
}