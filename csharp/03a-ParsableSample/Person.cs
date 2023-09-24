namespace ParseSample;

// nominal record type
public partial record class Person : IParsable<Person>, ISpanParsable<Person>, ISpanFormattable
{
    private static Person? s_person = default;
    public static Person Empty => s_person ??= 
        new Person { FirstName = string.Empty, LastName = string.Empty };

    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public string? MiddleName { get; init; }
}
