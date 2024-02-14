# ConsoleAppWithArguments

> Template for a C# console application that can be started with arguments or environment variables.
> It can be provided with default values or with an error message if the value is not set as a parameter or as an environment variable.

> Vorlage für eine C# Konsolen Applikation, die mit Argumenten oder mit Umgebungsvariablen gestartet werden kann.
> Sie kann mit Defaultwerten versehen werden oder mit einer Fehlermeldung, wenn der Wert nicht als Parameter oder als Umgebungsvariable gesetzt ist.

> Шаблон для консольного приложения на C#, которое можно запускать с помощью аргументов или переменных окружения.
> Он может иметь значения по умолчанию или сообщение об ошибке, если значение не задано в качестве параметра или переменной окружения.

The template is based on [System.CommandLine](https://www.nuget.org/packages/System.CommandLine) from Microsoft.

```csharp
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
```
