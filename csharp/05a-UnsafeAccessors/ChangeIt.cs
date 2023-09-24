using System.Runtime.CompilerServices;

namespace UnsafeAccessor;
internal class ChangeIt
{
    [UnsafeAccessor(UnsafeAccessorKind.Field, Name = "_title")]
    public extern static ref string GetTitle(Book @this);
}
