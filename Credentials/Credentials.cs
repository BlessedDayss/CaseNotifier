namespace CaseNotifier.Credentials;

public class Credentials
{
    public SiteCredentials SiteCre { get; set; }
    
    public class SiteCredentials
    {
        public string UserName { get; set; }
        public string UserPassword { get; set; }
    }
}
