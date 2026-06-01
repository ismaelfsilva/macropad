using System.Text.Json;

namespace Macropad.Core;

/// <summary>Loads macro definitions from JSON (an array of macros).</summary>
public static class MacroConfig
{
    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNameCaseInsensitive = true
    };

    /// <summary>Parses a JSON string into the list of macros.</summary>
    public static IReadOnlyList<Macro> Parse(string json) =>
        JsonSerializer.Deserialize<List<Macro>>(json, Options) ?? new List<Macro>();

    /// <summary>Reads and parses a JSON config file from <paramref name="path"/>.</summary>
    public static IReadOnlyList<Macro> Load(string path) =>
        Parse(File.ReadAllText(path));
}
