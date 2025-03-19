namespace CaseNotifier.Menu;

using Spectre.Console;

public static class Menu
{
    public static string MenuChoice()
    {
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[red]You can choice option:[/]")
                .AddChoices(new[] { "Return", "Exit" })
        );
        switch (choice)
        {
            case "Return":
                return "Return";
            case "Exit":
                return "Exit";
            default:
                return "Return";
        }
    }
}
