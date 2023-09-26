using System.Text;

namespace Bogus;

internal static class BogusExtensions
{
    /// <summary>
    /// Generates a dummy ISBN.
    /// </summary>
    public static string ISBN(this Faker f)
    {
        var sb = new StringBuilder(13+4);
        sb.Append("978-");
        sb.Append(f.Random.Number(9));
        sb.Append('-');
        sb.Append(f.Random.Number(1000, 9999));
        sb.Append('-');
        sb.Append(f.Random.Number(1000, 9999));
        sb.Append('-');
        sb.Append(f.Random.Number(9));
        return sb.ToString();
    }
}
