namespace CaseNotifier.Timer;

using Spectre.Console;

public static class Countdown
{
    private static CancellationTokenSource _cancellationTokenSource;

    public static async Task<bool> StartAsync(TimeSpan interval) {
        int totalSeconds = (int)interval.TotalSeconds;
        _cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = _cancellationTokenSource.Token;
        bool menuRequested = false;
        
        var keyMonitorTask = Task.Run(() => {
            while (!cancellationToken.IsCancellationRequested) {
                if (Console.KeyAvailable) {
                    var key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.M) {
                        menuRequested = true;
                        _cancellationTokenSource.Cancel();
                        return;
                    }
                }
                Thread.Sleep(100);
            }
        }, cancellationToken);

        try {
            await AnsiConsole.Progress().StartAsync(async ctx => {
                var progressTask = ctx.AddTask("[yellow]Waiting for next execution...[/]", autoStart: true,
                    maxValue: totalSeconds);

                progressTask.Description =
                    $"[gray]Next email in {totalSeconds} seconds[/] [green](Press 'M' for menu)[/]";

                while (!ctx.IsFinished && !cancellationToken.IsCancellationRequested) {
                    progressTask.Increment(1);
                    int remaining = totalSeconds - (int)progressTask.Value;

                    progressTask.Description =
                        $"[gray]Next email in {remaining} seconds[/] [green](Press 'M' for menu)[/]";

                    try {
                        await Task.Delay(1000, cancellationToken);
                    } catch (TaskCanceledException) {
                        break;
                    }
                }
            });
        } catch (OperationCanceledException) {
            Console.Clear(); 
        }
        return menuRequested;
    }
}