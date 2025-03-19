namespace CaseNotifier.Map;

public static class StatusMapper
{
    public static string MapStatus(string status)
    {
        return status switch
        {
            "ae5f2f10-f46b-1410-fd9a-0050ba5d6c38" => "NEW",
            "f063ebbe-fdc6-4982-8431-d8cfa52fedcf" => "Reopened",
            _ => "N/A"
        };
    }

    public static string MapStatusColor(string statusName) 
    {
        return statusName switch
        {
            "NEW"      => "green",  
            "Reopened" => "red",
            _          => "white"
        };
    }

    
    public static string GetColoredStatus(string status)
    {
        string name = MapStatus(status);
        string color = MapStatusColor(name);
        return $"[{color}]{name}[/]";
    }
}

