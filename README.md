# macropad

Configurable keyboard macro automation for Windows desktop, JSON-driven (C# / .NET).

`macropad` runs user-defined keyboard macros described in a JSON file. Each macro is
a named sequence of steps: press a key, type text, or wait. On Windows the keystrokes
are delivered to the active window via the `user32` `SendInput` API.

## Build

```sh
dotnet build
```

## Usage

```sh
# List macro names from a config file (defaults to macros.json)
macropad list [config.json]

# Run a macro by name (defaults to macros.json)
macropad run <name> [config.json]
```

Running a macro performs real keyboard input on Windows. `list` is safe everywhere.

## Config format

A config file is a JSON array of macros. Each macro has a `name` and a list of `steps`.
A step is exactly one of:

- `{ "key": "ENTER" }` — press a named key (e.g. `A`, `ENTER`, `TAB`, `ESC`, `SPACE`).
- `{ "text": "hello" }` — type a literal string.
- `{ "delayMs": 100 }` — wait the given number of milliseconds.

### Sample `macros.json`

```json
[
  {
    "name": "greet",
    "steps": [
      { "text": "hello world" },
      { "key": "ENTER" },
      { "delayMs": 100 },
      { "text": "from macropad" },
      { "key": "ENTER" }
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
```

## Project layout

- `src/Macropad.Core` — models, JSON config loader, macro runner, `IKeySender` / `WindowsKeySender`.
- `src/Macropad` — console application.
- `tests/Macropad.Tests` — xUnit tests (platform-agnostic, use a fake key sender).

## License

[MIT](LICENSE)
