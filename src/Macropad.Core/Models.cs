namespace Macropad.Core;

/// <summary>A single macro step: exactly one of Key, Text, or DelayMs is set.</summary>
public sealed class Step
{
    public string? Key { get; set; }
    public string? Text { get; set; }
    public int? DelayMs { get; set; }
}

/// <summary>A named, ordered sequence of steps.</summary>
public sealed class Macro
{
    public string Name { get; set; } = "";
    public List<Step> Steps { get; set; } = new();
}
