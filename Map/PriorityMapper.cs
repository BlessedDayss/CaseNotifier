namespace CaseNotifier.Map;

public class PriorityMapper
{
    public static string MapPriority(string priority)
    {
        return priority switch
        {
            "7e9f1204-f46b-1410-fb9a-0050ba5d6c38" => "Low",
            "d9bd322c-f46b-1410-ee8c-0050ba5d6c38" => "Medium",
            "8b700f24-f46b-1410-ee8c-0050ba5d6c38" => "High",
            "afb33208-f46b-1410-ec8c-0050ba5d6c38" => "Critical",
            _ => "N/A"
        };
    }
    public static string PriorityColorCode(string priorityName )
    {
        return priorityName switch
        {
            "Low"     => "blue",
            "Medium"  => "yellow",
            "High"    => "darkorange3", 
            "Critical"  => "red",
            _         => "white"
        };
    }
}
