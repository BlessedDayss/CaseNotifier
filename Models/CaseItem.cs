namespace CaseNotifier.Models;

public class CaseItem
{
    public string Number { get; set; }
    public string Subject { get; set; }
    public string Status { get; set; }
    public string PriorityId { get; set; }
    public DateTime  CreatedOn { get; set; }
}
