namespace CaseNotifier.Menu;

using Spectre.Console;

public static class Menu
{
    public static MenuAction MenuChoice() 
    {
        var choice = AnsiConsole.Prompt(new SelectionPrompt<string>().Title("[red]You can choice option:[/]")
            .AddChoices(new[] { "Return", "Exit" }));

        if (choice == "Return") 
        {
            bool confirm = AskConfirmation("Are you sure you want to return to the main menu?");
            if (confirm) 
            {
                return MenuAction.RechooseInterval;
            } else 
            {
                return MenuAction.Continue;
            }
        } else if (choice == "Exit") 
        {
            bool confirm = AskConfirmation("Are you sure you want to exit the program?");
            if (confirm) 
            {
                return MenuAction.Exit;
            } else 
            {
                return MenuAction.Continue;
            }
        }
        return MenuAction.Continue;
        
        static bool AskConfirmation(string message) 
        {
            var confirmChoice = AnsiConsole.Prompt(new SelectionPrompt<string>().Title($"[red]{message}[/]")
                .AddChoices(new[] { "Yes", "No" }));
            return confirmChoice == "Yes";
        }
    }
}
