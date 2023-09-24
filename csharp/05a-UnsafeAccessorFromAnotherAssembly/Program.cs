using System.Runtime.CompilerServices;

Book b1 = new("A new book");
b1.GetTitle() = "a new title";
Console.WriteLine(b1);

static class ChangeIt2
{
    [UnsafeAccessor(UnsafeAccessorKind.Field, Name = "_title")]
    public extern static ref string GetTitle(this Book @this);
}