using System;
using System.IO;
using System.Threading.Tasks;
using CaseNotifier.Credentials;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ODataReader
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Загружаем конфигурацию из credentials.json.
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("credentials.json", optional: false)
                .Build();

            // Десериализуем данные в объект CredentialsWrapper.
            CredentialsWrapper credentialsWrapper = null;
            try
            {
                string credentialsJson = File.ReadAllText("credentials.json");
                credentialsWrapper = JsonConvert.DeserializeObject<CredentialsWrapper>(credentialsJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading credentials.json: " + ex.Message);
                return;
            }

            var authCredentials = credentialsWrapper?.Auth;
            if (authCredentials == null)
            {
                Console.WriteLine("Failed to load authentication credentials.");
                return;
            }

            // Выполняем авторизацию, используя данные из credentials.json.
            var client = await LoginService.LoginAsync(authCredentials);
            if (client == null)
            {
                Console.WriteLine("Login failed.");
                return;
            }

            // Далее можно использовать авторизованный client для выполнения запросов.
            Console.WriteLine("Further requests will be made using the authenticated HttpClient.");
        }
    }
}