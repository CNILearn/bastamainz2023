using UnsafeAccessor;

Book b1 = new("Pragmatic Microservices");
Console.WriteLine(b1);

// set the private field using this unsafe accessor
ChangeIt.GetTitle(b1) = "Pragmatic Microservices with .NET and Azure";
Console.WriteLine(b1);

public class Book(string title)
{
    private readonly string _title = title;
    public override string ToString() => _title;
}