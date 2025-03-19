
namespace CaseNotifier
{
    using System.Runtime.InteropServices.JavaScript;
    using CaseNotifier.Services;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    class Program
    {
        static async Task Main(string[] args)
        {
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                .Build();
            

            var services = new ServiceCollection();
            services.AddSingleton<IConfiguration>(config);
            
            // var serviceProvider = services.BuildServiceProvider();
            HttpClient client = await AuthService.LoginAsync(config);
            
            
        }
    }
}
