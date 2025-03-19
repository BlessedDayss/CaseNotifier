using System.Net;
            using System.Net.Mail;
            using System.Text;
            using Microsoft.Extensions.Configuration;
            using Newtonsoft.Json.Linq;
            
            namespace CaseNotifier
            {
                using CaseNotifier.Models;
            
                public class EmailNotificationService
                {
                    private readonly IConfiguration _configuration;
            
                    public EmailNotificationService(IConfiguration configuration)
                    {
                        _configuration = configuration;
                    }
            
                    public async Task SendEmailAsync(string subject, string jsonData)
                    {
                        JObject jObject;
                        try
                        {
                            jObject = JObject.Parse(jsonData);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Error parsing JSON: " + ex.Message, ex);
                        }
            
                        var valueToken = jObject["value"];
                        if (valueToken == null)
                        {
                            throw new Exception("Field 'value' not found in JSON.");
                        }
                        
                        var items = valueToken.ToObject<List<CaseItem>>();
                        if (items == null || items.Count == 0)
                        {
                            throw new Exception("No data in 'value' array.");
                        }
                        
                        string htmlBody = HtmlFormatter.CreateHtmlTable(items);
                        
                        var smtpHost    = _configuration["Email:SmtpHost"]       ?? throw new Exception("SmtpHost not specified in configuration.");
                        var smtpPortRaw = _configuration["Email:SmtpPort"]       ?? throw new Exception("SmtpPort not specified in configuration.");
                        var enableSslRaw= _configuration["Email:EnableSsl"]      ?? "true"; // default is true
                        var username    = _configuration["Email:Username"]       ?? throw new Exception("Username not specified in configuration.");
                        var password    = _configuration["Email:Password"]       ?? throw new Exception("Password not specified in configuration.");
                        var fromAddress = _configuration["Email:FromAddress"]    ?? throw new Exception("FromAddress not specified in configuration.");
                        var toAddress   = _configuration["Email:ToAddress"]      ?? throw new Exception("ToAddress not specified in configuration.");
            
                        if (!int.TryParse(smtpPortRaw, out var smtpPort))
                        {
                            throw new Exception($"Invalid SmtpPort value: {smtpPortRaw}");
                        }
                        if (!bool.TryParse(enableSslRaw, out var enableSsl))
                        {
                            throw new Exception($"Invalid EnableSsl value: {enableSslRaw}");
                        }
                        
                        using (var client = new SmtpClient(smtpHost, smtpPort))
                        {
                            client.EnableSsl = enableSsl;
                            client.Credentials = new NetworkCredential(username, password);
                            
                            using (var mailMessage = new MailMessage(fromAddress, toAddress, subject, htmlBody))
                            {
                                mailMessage.IsBodyHtml = true; 
            
                                try
                                {
                                    await client.SendMailAsync(mailMessage);
                                }
                                catch (Exception ex)
                                {
                                    throw new Exception($"Error when sending email: {ex.Message}", ex);
                                }
                            }
                        }   
                    }
                }
                
            }