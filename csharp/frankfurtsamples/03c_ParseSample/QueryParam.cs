using System.Globalization;

namespace ParseSample;

public struct QueryParam<T>
    where T : ISpanParsable<T>
{
    private T _value;
    public QueryParam(ReadOnlySpan<char> value, T defaultValue)
    {
        if (!T.TryParse(value, CultureInfo.InvariantCulture, out T? result))
        {
            _value = defaultValue;
        }
        else
        {
            _value = result;
        }
    }

    public T Value
    {
        get => _value;
        set => _value = value;
    }
}
