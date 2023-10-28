using BooksStorage.Models.Mail;

namespace BooksStorage.AsyncServices;

public interface IHttpEmailClient
{
    Task SendMailRequest(Email email);
}