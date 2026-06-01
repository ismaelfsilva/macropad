namespace Macropad.Core;

/// <summary>Abstraction over keyboard output so macros can be tested without real input.</summary>
public interface IKeySender
{
    void SendKey(string key);
    void SendText(string text);
}
