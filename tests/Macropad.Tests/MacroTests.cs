using Macropad.Core;

namespace Macropad.Tests;

public class MacroConfigTests
{
    private const string SampleJson = """
    [
      {
        "name": "greet",
        "steps": [
          { "text": "hello" },
          { "key": "ENTER" },
          { "delayMs": 100 },
          { "text": "world" }
        ]
      },
      {
        "name": "save",
        "steps": [
          { "key": "CTRL" },
          { "key": "S" }
        ]
      }
    ]
    """;

    [Fact]
    public void Parse_YieldsExpectedMacroAndStepCounts()
    {
        var macros = MacroConfig.Parse(SampleJson);

        Assert.Equal(2, macros.Count);
        Assert.Equal("greet", macros[0].Name);
        Assert.Equal(4, macros[0].Steps.Count);
        Assert.Equal("save", macros[1].Name);
        Assert.Equal(2, macros[1].Steps.Count);
    }

    [Fact]
    public void Parse_MapsStepFields()
    {
        var greet = MacroConfig.Parse(SampleJson)[0];

        Assert.Equal("hello", greet.Steps[0].Text);
        Assert.Equal("ENTER", greet.Steps[1].Key);
        Assert.Equal(100, greet.Steps[2].DelayMs);
    }
}

public class MacroRunnerTests
{
    [Fact]
    public void Run_InvokesSenderInOrder_AndHonorsDelays()
    {
        var macro = MacroConfig.Parse("""
        [
          {
            "name": "m",
            "steps": [
              { "text": "hi" },
              { "key": "ENTER" },
              { "delayMs": 250 },
              { "key": "A" }
            ]
          }
        ]
        """)[0];

        var sender = new FakeKeySender();
        var delays = new List<int>();
        var runner = new MacroRunner(sender, delays.Add);

        runner.Run(macro);

        Assert.Equal(new[] { "text:hi", "key:ENTER", "key:A" }, sender.Calls);
        Assert.Equal(new[] { 250 }, delays);
    }
}
