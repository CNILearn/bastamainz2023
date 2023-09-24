namespace Positional;

// Positional records
public record class Person(string FirstName, string LastName, string? Middlename = default);

public record class Student(string FirstName, string LastName, string Course, string? Middlename = default)
    : Person(FirstName, LastName, Middlename);