
namespace CaseNotifier
{
    using System.Runtime.InteropServices.JavaScript;
    using CaseNotifier.Menu;
    using CaseNotifier.Services;
    using CaseNotifier.Timer;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Spectre.Console;

    class Program
    {
        static async Task Main(string[] args) {
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
            IConfiguration config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true).Build();


            var services = new ServiceCollection();
            services.AddSingleton<IConfiguration>(config);
            services.AddSingleton<EmailNotificationService>();
            var serviceProvider = services.BuildServiceProvider();
            HttpClient client = await AuthService.LoginAsync(config);

            var emailService = serviceProvider.GetRequiredService<EmailNotificationService>();

            while (true) {

                int intevalMinutes = TimeInterval.GetInterval();
                TimeSpan interval = TimeSpan.FromMinutes(intevalMinutes);

                while (true) {
                    Console.Clear();
                    AnsiConsole.MarkupLine($"[purple]Interval chosen: {intevalMinutes} minutes[/]");
                    string odataResponse = await OdataRequest.SendRequestAsync(config, client);
                    string subject = $"Case Notification - {DateTime.Now:yyyy-MM-dd HH:mm}";

                    await emailService.SendEmailAsync(subject, odataResponse);
                    AnsiConsole.MarkupLine($"\n[purple]Email sent successfully![/]");

                    bool menuRequested = await Countdown.StartAsync(interval);

                    MenuAction action = Menu.Menu.MenuChoice();
                    switch (action) {
                        case MenuAction.RechooseInterval:
                            break;
                        case MenuAction.Exit:
                            return;
                        case MenuAction.Continue:
                            continue;
                    }
                    break;
                }
            }
        }
    }
}