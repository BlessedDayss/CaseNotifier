using System.Text;
using CaseNotifier.Map;     // Где лежат PriorityMapper и StatusMapper
using CaseNotifier.Models;

public static class HtmlFormatter
{
    public static string CreateHtmlTable(List<CaseItem> items)
    {
        var sb = new StringBuilder();
        sb.Append("<html><head>");
        sb.Append("<style>");
        sb.Append("table { border-collapse: collapse; width: 100%; }");
        sb.Append("th, td { border: 1px solid #ddd; padding: 8px; text-align: left; }");
        sb.Append("th { background-color: #f2f2f2; }");
        sb.Append("tr:nth-child(even) { background-color: #f9f9f9; }");
        sb.Append("</style>");
        sb.Append("</head><body>");
        sb.Append("<h2>Case Notifications</h2>");
        sb.Append("<table>");
        sb.Append("<tr><th>Number</th><th>Subject</th><th>Priority</th><th>Status</th><th>Created On</th></tr>");

        foreach (var item in items)
        {
            
            string priorityName = PriorityMapper.MapPriority(item.PriorityId ?? string.Empty);
            string priorityColor = PriorityMapper.PriorityColorCode(priorityName);

            
            string statusName = StatusMapper.MapStatus(item.StatusId ?? string.Empty);
            string statusColor = StatusMapper.MapStatusColor(statusName);

            
            sb.Append("<tr>");
            sb.Append($"<td>{item.Number ?? "N/A"}</td>");
            sb.Append($"<td>{item.Subject ?? "N/A"}</td>");

            
            sb.Append($"<td style=\"color:{priorityColor};\">{priorityName}</td>");

           
            sb.Append($"<td style=\"color:{statusColor};\">{statusName}</td>");

            sb.Append($"<td>{item.CreatedOn:yyyy-MM-dd HH:mm}</td>");
            sb.Append("</tr>");
        }

        sb.Append("</table>");
        sb.Append("</body></html>");
        return sb.ToString();
    }
}
