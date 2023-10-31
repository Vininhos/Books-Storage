using System.Net.Mail;
using BooksStorage.Mail.Models;

namespace BooksStorage.Mail.Data;

public class MailRepository : IMailRepository
{
    private readonly IConfiguration _configuration;

    public MailRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<bool> SendMail(Email email)
    {
        MailMessage mailMessage = new MailMessage(email.From, email.To);

        mailMessage.Subject = email.Subject;
        mailMessage.Body = email.Body;
        SmtpClient smtpClient = new SmtpClient(_configuration["Address"], int.Parse(_configuration["Port"]));
        
        try
        {
           await smtpClient.SendMailAsync(mailMessage);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to send email: {0}", ex);
            return false;
        }

        return true;
    }
}