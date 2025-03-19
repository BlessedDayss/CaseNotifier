namespace CaseNotifier.Timer;

using Spectre.Console;

public class TimeInterval
{
    public static int GetInterval() 
    {
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[red]Please choose an interval in minutes:[/]")
                .AddChoices(new[] { "5 minutes", "10 minutes", "15 minutes", "30 minutes", "60 minutes" })
        );

        return choice switch {
            "5 minutes" => 5,
            "10 minutes" => 10,
            "15 minutes" => 15,
            "30 minutes" => 30,
            "60 minutes" => 60
        };
    }
}
