namespace Microsoft.Extensions.Configuration;

internal static class ConfigurationExtensions
{
    public static string GetRequired(this IConfiguration configuration, string key) =>
        configuration[key] ?? throw new InvalidOperationException($"Configuration \"{key}\" not found");
}
