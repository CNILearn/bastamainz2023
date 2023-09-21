using System.IO.Compression;
using System.Reflection;
using System.Text.Json;

if (args is null || args.Length != 1)
{
    Console.WriteLine("Pass the directory to create the JSON file");
    return;
}

Result result = new();

foreach (var fileName in Directory.EnumerateFiles(args[0], "*.nupkg"))
{
    Console.WriteLine($"package {fileName}");
    using ZipArchive archive = ZipFile.OpenRead(fileName);
    foreach (var entry in archive.Entries
        .Where(entry => entry.Name.EndsWith("dll")))
    {
        Console.WriteLine(entry.FullName);

        using var stream = entry.Open();
        Assembly assembly = Assembly.Load(ReadFromStream(stream));
        try
        {
            var types = assembly.GetExportedTypes();
            foreach (var type in types)
            {
                Console.WriteLine($"type: {type.FullName}");

                Type? ti = type.GetInterface("IInitialize");
                if ( ti != null )
                {
                    Console.WriteLine($"Found interface IInitialize implemented with type {type.FullName}");
                    result.Types.Add(new TypeInformation(type.Namespace ?? string.Empty, type.Name));
                }
            }
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    Console.WriteLine();
}

byte[] bytes = JsonSerializer.SerializeToUtf8Bytes(result);
File.WriteAllBytes("result.json", bytes);
Console.WriteLine("file result.json written");

static byte[] ReadFromStream(Stream input)
{
    using MemoryStream ms = new();
    input.CopyTo(ms);
    return ms.ToArray();
}

#pragma warning disable CA1050 // Declare types in namespaces
public class Result
{
    public IList<TypeInformation> Types { get; } = new List<TypeInformation>();
}

public record TypeInformation(string Namespace, string Type);
#pragma warning restore CA1050 // Declare types in namespaces