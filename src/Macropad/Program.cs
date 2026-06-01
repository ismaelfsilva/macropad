using Macropad.Core;

const string usage = "Usage:\n  macropad list [config.json]\n  macropad run <name> [config.json]";

if (args.Length == 0)
{
    Console.Error.WriteLine(usage);
    return 1;
}

try
{
    switch (args[0].ToLowerInvariant())
    {
        case "list":
        {
            string path = args.Length > 1 ? args[1] : "macros.json";
            foreach (var macro in MacroConfig.Load(path))
                Console.WriteLine(macro.Name);
            return 0;
        }
        case "run":
        {
            if (args.Length < 2)
            {
                Console.Error.WriteLine(usage);
                return 1;
            }
            string name = args[1];
            string path = args.Length > 2 ? args[2] : "macros.json";
            var macro = MacroConfig.Load(path)
                .FirstOrDefault(m => string.Equals(m.Name, name, StringComparison.OrdinalIgnoreCase));
            if (macro is null)
            {
                Console.Error.WriteLine($"Macro not found: {name}");
                return 1;
            }
            new MacroRunner(new WindowsKeySender()).Run(macro);
            Console.WriteLine($"Ran macro '{macro.Name}' ({macro.Steps.Count} steps).");
            return 0;
        }
        default:
            Console.Error.WriteLine(usage);
            return 1;
    }
}
catch (Exception ex)
{
    Console.Error.WriteLine($"Error: {ex.Message}");
    return 1;
}
