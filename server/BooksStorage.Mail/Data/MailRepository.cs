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
    try
    {
      _logger.LogInformation("Starting email send process and Email Message build.");

      MailMessage mailMessage = new MailMessage(email.From, email.To);
      mailMessage.Subject = email.Subject;
      mailMessage.Body = email.Body;

      _logger.LogInformation("Email Message was successfully created.");

      SmtpClient smtpClient = new SmtpClient(_mailHogSettings.Address, _mailHogSettings.Port);

      _logger.LogInformation("Starting email send process.");

      smtpClient.Send(mailMessage);
    }
    catch (Exception ex)
    {
      _logger.LogError("Failed to send email. Exception: {Ex}", ex);

      return Task.FromResult(false);
    }

    _logger.LogInformation("Email was sent successfully!");

    return Task.FromResult(true);
  }
}
