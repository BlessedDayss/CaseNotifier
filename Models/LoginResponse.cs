namespace CaseNotifier.Models;

public class LoginResponse
{
    public int Code { get; set; }
    public string Message { get; set; }
    public object Exception { get; set; }
}
