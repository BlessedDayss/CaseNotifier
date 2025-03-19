﻿namespace CaseNotifier.Models;

public class EmailOptions
{
    public string SmtpServer { get; set; }
    public int Port { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string From { get; set; }
    public string To { get; set; }
}
