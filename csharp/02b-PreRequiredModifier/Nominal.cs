using System.Diagnostics.CodeAnalysis;

namespace Nominal;

public class Person
{
    public Person()
    {
    }

    public Person(string first, string last) =>
        (FirstName, LastName) = (first, last);

    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string? MiddleName { get; init; }
}