using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Spectre.Console;

public class OdataRequest
{
    public static async Task<string> SendRequestAsync(IConfiguration config) {
        string defaultUrl = config["Url:DefaultUrl"];
        string odataUrl = config["Odata:OdataUrl"];

        string fullUrl = $"{defaultUrl}/{odataUrl}";

        var client = new HttpClient();
        var response = await client.GetAsync(fullUrl);
        AnsiConsole.MarkupLine("Sending request...");
        AnsiConsole.MarkupLine($"[bold]Request URL[/]: {fullUrl}");
        AnsiConsole.WriteLine("Response: " + (int)response.StatusCode);
        

        return await response.Content.ReadAsStringAsync();
    }
}