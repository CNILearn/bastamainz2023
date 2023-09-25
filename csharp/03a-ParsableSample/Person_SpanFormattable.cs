namespace ParseSample;

public partial record class Person : ISpanFormattable
{
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

    public override string ToString() => this switch
    {
        { MiddleName: null } => $"{FirstName} {LastName}",
        _ => $"{FirstName} {MiddleName} {LastName}"
    };

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
}
