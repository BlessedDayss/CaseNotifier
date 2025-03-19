
namespace CaseNotifier
{
    using System.Runtime.InteropServices.JavaScript;
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

            // var serviceProvider = services.BuildServiceProvider();
            HttpClient client = await AuthService.LoginAsync(config);

            Console.Clear();
            int interval = TimeInterval.GetInterval();
            AnsiConsole.MarkupLine($"[purple]You have chosen {interval} minutes[/]");
            
           string odataResponse = await OdataRequest.SendRequestAsync(config);
           AnsiConsole.MarkupLine("[bold]Response[/]: " + odataResponse);

            // var table = new Table();
            // table.Border = TableBorder.Double;
            // table.AddColumn("Case Number");
            // table.AddColumn("Case Title");
            // table.AddColumn("Case Subject");
            // table.AddColumn("Case Priority");
            // table.AddColumn("Status");
            //
            // AnsiConsole.Write(table);
            //
            //
            // Console.ReadKey();

        }
    }
}