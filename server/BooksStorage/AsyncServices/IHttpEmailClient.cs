namespace BooksStorage.AsyncServices;

public interface IHttpEmailClient
{
    Task SendMailRequest(Mail.Models.Mail mail);
}