using System.Diagnostics.CodeAnalysis;

namespace ParseSample;

public partial record class Person : IParsable<Person>
{
    public static Person Parse(string s, IFormatProvider? provider = default)
    {
        if (!TryParse(s, provider, out Person? result))
        {
            throw new FormatException();
        }
        return result;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Person result)
    { 
        string[]? names = s?.Split(' ');
        result = names switch
        {
            { Length: 2 } => new Person { FirstName = names[0], LastName = names[1] },
            { Length: 3 } => new Person { FirstName = names[0], MiddleName = names[1], LastName = names[2] },
            _ => null
        };
        return result is not null;
    }
}
