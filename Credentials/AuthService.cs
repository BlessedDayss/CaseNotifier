namespace CaseNotifier.Services
{
    using System.Data.SqlTypes;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Options;
    using CaseNotifier.Models;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using Spectre.Console;

    public static class AuthService
    {
        public static async Task<HttpClient> LoginAsync(IConfiguration config) {
            string defaultUrl = config["Url:DefaultUrl"];
            string authUrl = config["Authentication:authorizationUrl"];
            string userName = config["Credentials:UserName"];
            string userPassword = config["Credentials:UserPassword"];


            string fullUrl = $"{defaultUrl}/{authUrl}";


            var requestBody = new {
                UserName = userName,
                UserPassword = userPassword
            };
            AnsiConsole.MarkupLine($"Login: [bold]UserName[/]: {userName}");
            AnsiConsole.MarkupLine($"Login: [bold]Password[/]: {userPassword}");
            AnsiConsole.MarkupLine("Logging in...");
            

            var json = JsonConvert.SerializeObject(requestBody);

            var content = new StringContent(json, Encoding.UTF8, "application/json");


            var client = new HttpClient();
            var response = await client.PostAsync(fullUrl, content);

            string responseString = await response.Content.ReadAsStringAsync();

            LoginResponse loginResponse = JsonConvert.DeserializeObject<LoginResponse>(responseString);

            if (loginResponse.Code != 0) {
                AnsiConsole.MarkupLine($"Error: {loginResponse.Message}");
            } else {
                AnsiConsole.MarkupLine($"[blue]Login successful[/]");
            }
            return client;
        }
    }
}