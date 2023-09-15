namespace DefaultInterfaceMethods;

public interface IEnumerableEx<T> : IEnumerable<T>
{
    public virtual IEnumerable<T> Where(Func<T, bool> pred)
    {
        foreach (T item in this)
        {
            if (pred(item))
            {
                yield return item;
            }
        }
    }
}
