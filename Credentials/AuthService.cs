namespace CaseNotifier.Services
{
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
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
            

            var json = JsonConvert.SerializeObject(requestBody);

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            var handler =  new HttpClientHandler{UseCookies = true};
            var client = new HttpClient(handler);
            
            var response = await client.PostAsync(fullUrl, content);

            string responseString = await response.Content.ReadAsStringAsync();

            LoginResponse loginResponse = JsonConvert.DeserializeObject<LoginResponse>(responseString);

            if (loginResponse.Code != 0) {
                AnsiConsole.MarkupLine($"Error: {loginResponse.Message}");
                Environment.Exit(1);

            } else {
                AnsiConsole.MarkupLine($"[blue]Login successful[/]");
            }
            
            var cookies = handler.CookieContainer.GetCookies(new Uri(defaultUrl));
            // var csrfToken = cookies[".AspNetCore.CsrfToken"];
            
            return client;
        }
    }
}