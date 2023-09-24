using System.Diagnostics.CodeAnalysis;

namespace Nominal;

public class Person
{
    public Person()
    {
    }

    [SetsRequiredMembers]
    public Person(string first, string last) =>
        (FirstName, LastName) = (first, last);

    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public string? MiddleName { get; init; }
}