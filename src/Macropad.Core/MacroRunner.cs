namespace Macropad.Core;

/// <summary>Executes a macro's steps against an <see cref="IKeySender"/>.</summary>
public sealed class MacroRunner
{
    private readonly IKeySender _sender;
    private readonly Action<int> _delay;

    /// <param name="sender">Target for key/text output.</param>
    /// <param name="delay">Delay handler (milliseconds); defaults to <see cref="Thread.Sleep(int)"/>.
    /// Injectable so tests run instantly.</param>
    public MacroRunner(IKeySender sender, Action<int>? delay = null)
    {
        _sender = sender;
        _delay = delay ?? Thread.Sleep;
    }

    public void Run(Macro macro)
    {
        foreach (var step in macro.Steps)
        {
            if (step.DelayMs is int ms)
                _delay(ms);
            else if (step.Text is not null)
                _sender.SendText(step.Text);
            else if (step.Key is not null)
                _sender.SendKey(step.Key);
        }
    }
}
