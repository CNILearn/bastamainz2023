using System.Diagnostics.CodeAnalysis;

namespace ParseSample;

public partial record class Person : ISpanParsable<Person>
{
    public static Person Parse(ReadOnlySpan<char> s, IFormatProvider? provider = default)
    {
        if (!TryParse(s, provider, out Person? result))
        {
            throw new FormatException();
        }
        return result;
    }

    public static bool TryParse(ReadOnlySpan<char> s, [MaybeNullWhen(false)] out Person result) =>
        TryParse(s, null, out result);

    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out Person result)
    {
        int index = s.IndexOf(' ');
        if (index < 0)
        {
            result = null;
            return false;
        }
        var first = s[..index];
        var remaining = s[(index+1)..];
        index = remaining.IndexOf(' ');
        if (index < 0)
        {
            result = new Person { FirstName = first.ToString(), LastName = remaining.ToString() };
            return true;
        }
        else
        {
            var middle = remaining[..index];
            var last = remaining[(index+1)..];
            result = new Person
            {
                FirstName = first.ToString(),
                MiddleName = middle.ToString(),
                LastName = last.ToString(),
            };
            return true;
        }
    }
}
