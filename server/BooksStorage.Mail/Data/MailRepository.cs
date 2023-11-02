using System.Net.Mail;
using BooksStorage.Mail.Models;
using Microsoft.Extensions.Options;

namespace BooksStorage.Mail.Data;

public class MailRepository : IMailRepository
{
    private readonly MailHogSettings _mailHogSettings;

    public MailRepository(IOptions<MailHogSettings> mailHogSettings)
    {
        _mailHogSettings = new();
        _mailHogSettings.Address = mailHogSettings.Value.Address;
        _mailHogSettings.Port = mailHogSettings.Value.Port;
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
            Console.WriteLine("Failed to send email: {0}", ex);
            return Task.FromResult(false);
        }

        return Task.FromResult(true);
    }
}