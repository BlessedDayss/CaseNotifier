using System.Diagnostics;
using System.Text;
using Newtonsoft.Json;

namespace CaseNotifier.Credentials;

public class LoginService
{
    public static async Task<HttpClient> LoginAsync(AuthCredentials auth)
    {
        if (auth == null)
        {
            Console.WriteLine("AuthCredentials is null");
            return null;
        }

        if (string.IsNullOrWhiteSpace(auth.Username) ||
            string.IsNullOrWhiteSpace(auth.Password) ||
            string.IsNullOrWhiteSpace(auth.Url))
        {
            Console.WriteLine("AuthCredentials is missing required fields");
            return null;
        }

        var namePass = new
        {
            UserName = auth.Username,
            UserPassword = auth.Password
        };

        string jsonNamePass = JsonConvert.SerializeObject(namePass);
        Console.WriteLine($"Logging in with {jsonNamePass} to {auth.Url}");

        var client = new HttpClient();

        try
        {
            var response = await client.PostAsync(auth.Url,
                new StringContent(jsonNamePass, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Login response: " + responseBody);

            Console.WriteLine("Login successful");
            return client;
        }
        catch (Exception e)
        {
            Console.WriteLine("Login failed: " + e.Message);
            return null;
        }
    }
}