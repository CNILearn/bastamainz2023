using System.Diagnostics.CodeAnalysis;

namespace ParseSample;

// nominal record type
public record class Person : IParsable<Person>, ISpanParsable<Person>, ISpanFormattable
{
    private static Person? s_person = default;
    public static Person Empty => s_person ??= 
        new Person { FirstName = string.Empty, LastName = string.Empty };

    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public string? MiddleName { get; init; }

    #region IParsable
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
        var names = s?.Split(' ');
        result = names switch
        {
            { Length: 2 } => new Person { FirstName = names[0], LastName = names[1] },
            { Length: 3 } => new Person { FirstName = names[0], MiddleName = names[1], LastName = names[2] },
            _ => null
        };
        return result is not null;
    }
    #endregion

    #region ISpanParsable
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
        var index = s.IndexOf(' ');
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

    #endregion

    #region ISpanFormattable
    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider = default)
    {
        // pattern match with Span<T> - new since C# 11!!
        ReadOnlySpan<char> dest = format switch
        {
            "F" => FirstName.AsSpan(),
            "L" => LastName.AsSpan(),
            "M" => MiddleName.AsSpan(),
            _ => null
        };
        if (dest.IsEmpty)
        {
            charsWritten = 0;
            return false;
        }
        else
        {
            dest.CopyTo(destination);
            charsWritten = dest.Length;
            return true;
        }
    }

    public override string ToString()
    {
        return this switch
        {
            { MiddleName: null } => $"{FirstName} {LastName}",
            _ => $"{FirstName} {MiddleName} {LastName}"
        };
    }

    public string ToString(string? format, IFormatProvider? formatProvider = default) => 
        format switch
        {
            "F" => FirstName,
            "L" => LastName,
            "M" => MiddleName ?? string.Empty,
            "FL" => $"{FirstName} {LastName}",
            null => ToString(),
            _ => throw new FormatException()
        };
    #endregion
}
