using System.CommandLine;

var prefix = "TRXPARSER";

// Title with default value as string
var title = new Option<string>
    (name: "--title",
    description: "The title of the test run",
    getDefaultValue: () => Environment.GetEnvironmentVariable($"{prefix}_TITLE") ?? "Title argument not set!");

// Searchpath with exception when not set
var searchpath = new Option<string>
    (name: "--searchpath",
    description: "File path to search the TRX files",
    getDefaultValue: () => Environment.GetEnvironmentVariable($"{prefix}_SEARCHPATH") ?? throw new Exception("\n\n=>Search path for TRX files not set!\n"));

// OKLimit with default value as int
var oklimit = new Option<int>
    (name: "--oklimit",
    description: "Minimum percentage as Int for a test to be marked as OK",
    getDefaultValue: () => int.TryParse(Environment.GetEnvironmentVariable($"{prefix}_OKLIMIT"), out int limit) ? limit : 100);

var rootCommand = new RootCommand
{
    title,
    searchpath,
    oklimit
};

rootCommand.SetHandler((title, searchpath, oklimit) =>
{
    Console.WriteLine(searchpath);
    Console.WriteLine(oklimit);
    Console.WriteLine(title);
}, title, searchpath, oklimit);

await rootCommand.InvokeAsync(args);