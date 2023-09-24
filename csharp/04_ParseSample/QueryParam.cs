using System.Globalization;

namespace ParseSample;

public struct QueryParam<T>
    where T : ISpanParsable<T>
{

    public QueryParam(ReadOnlySpan<char> value, T defaultValue)
    {
        if (!T.TryParse(value, CultureInfo.InvariantCulture, out T? result))
        {
            Value = defaultValue;
        }
        else
        {
            Value = result;
        }
    }

    public T Value { get; set; }
}
