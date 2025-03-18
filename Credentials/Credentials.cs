namespace CaseNotifier.Credentials;

public class Credentials
{
    public SiteCredentials loginAndPass { get; set; }
    
    public class SiteCredentials
    {
        public string UserName { get; set; }
        public string UserPassword { get; set; }
    }
}
