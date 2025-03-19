using CaseNotifier.Map;
using CaseNotifier.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Spectre.Console;

public class OdataRequest
{
    public static async Task<string> SendRequestAsync(IConfiguration config, HttpClient client) {
        string defaultUrl = config["Url:DefaultUrl"];
        string odataUrl = config["Odata:OdataUrl"];
        
        string fullUrl = $"{defaultUrl}/{odataUrl}";

        HttpResponseMessage response = await client.GetAsync(fullUrl);
        
        
        var content = await response.Content.ReadAsStringAsync();

        try {
            var jsonRepsonse = JObject.Parse(content);
            var cases = jsonRepsonse["value"].ToObject<List<CaseItem>>();


            var table = new Table()
                .Border(TableBorder.DoubleEdge)
                .BorderColor(Color.BlueViolet);
            table.AddColumn("Case Number");
            table.AddColumn("Subject");
            table.AddColumn("Priority");
            table.AddColumn("Status");
            table.AddColumn("Created On");

            foreach (var caseItem in cases) {
                string coloredPriority = PriorityMapper.GetColoredPriority(caseItem.PriorityId);
                string coloredStatus = StatusMapper.GetColoredStatus(caseItem.StatusId);
                table.AddRow(
                    caseItem.Number ?? "N/A",
                    caseItem.Subject ?? "N/A",
                    coloredPriority,
                    coloredStatus,
                    caseItem.CreatedOn.ToString("yyyy-MM-dd HH:mm")
                    );
                
            }AnsiConsole.Write(table);
        
        } catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
        
        return await response.Content.ReadAsStringAsync();
    }
}