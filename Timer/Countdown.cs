namespace CaseNotifier.Timer;

using Spectre.Console;

    public static class Countdown
    {
        public static async Task StartAsync(TimeSpan interval) {
            int totalSeconds = (int)interval.TotalSeconds;

            await AnsiConsole.Progress().StartAsync(async ctx => {
                var progressTask = ctx.AddTask("[yellow]Waiting for next execution...[/]", autoStart: true,
                    maxValue: totalSeconds);

                while (!ctx.IsFinished) {
                    progressTask.Increment(1);
                    int remaining = totalSeconds - (int)progressTask.Value;
                    
                    progressTask.Description = $"[gray]Next email in {remaining} seconds[/]";

                    await Task.Delay(1000);
                }
            });
        }
    }