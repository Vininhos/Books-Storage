using System.Net.Mail;
using BooksStorage.Mail.Models;
using Microsoft.Extensions.Options;

namespace BooksStorage.Mail.Data;

public class MailRepository : IMailRepository
{
    private readonly MailHogSettings _mailHogSettings;
    private readonly ILogger<MailRepository> _logger;

    public MailRepository(IOptions<MailHogSettings> mailHogSettings, ILogger<MailRepository> logger)
    {
        _mailHogSettings = new();
        _mailHogSettings.Address = mailHogSettings.Value.Address;
        _mailHogSettings.Port = mailHogSettings.Value.Port;
        _logger = logger;
    }

    public Task<bool> SendMail(Email email)
    {
        MailMessage mailMessage = new MailMessage(email.From, email.To);
        mailMessage.Subject = email.Subject;
        mailMessage.Body = email.Body;
        
        SmtpClient smtpClient = new SmtpClient(_mailHogSettings.Address, _mailHogSettings.Port);
        
        try
        { 
            smtpClient.Send(mailMessage);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send email for {Email}", email);
            return Task.FromResult(false);
        }

        return Task.FromResult(true);
    }
}