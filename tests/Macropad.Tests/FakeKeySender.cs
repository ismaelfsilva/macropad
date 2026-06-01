using Macropad.Core;

namespace Macropad.Tests;

/// <summary>Records every call so tests can assert the executed sequence.</summary>
public sealed class FakeKeySender : IKeySender
{
    public List<string> Calls { get; } = new();

    public void SendKey(string key) => Calls.Add($"key:{key}");
    public void SendText(string text) => Calls.Add($"text:{text}");
}
