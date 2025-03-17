namespace CaseNotifier.Credentials;

using System.Threading.Channels;
using Newtonsoft.Json;

public class LoginService
{
    public static async Task<HttpClient> LoginAsync(string urlRoot, Credentials credentials) 
    {
        if (credentials == null || credentials.SiteCre ==null)
        {
            Console.WriteLine("Failed to login");
            return null;
        } 
        
        if (string.IsNullOrWhiteSpace(credentials.SiteCre.UserName ) || string.IsNullOrWhiteSpace(credentials.SiteCre.UserPassword))
        {
            Console.WriteLine("Failed to login");
            return null;
        }


        var load = new {
            username = credentials.SiteCre.UserName,
            password = credentials.SiteCre.UserPassword
        };
        
        string jsonLoad = JsonConvert.SerializeObject(load);
    }
}
