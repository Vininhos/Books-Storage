using BooksStorage.Mail.Models;

namespace BooksStorage.Mail.Data;

public interface IMailRepository
{
    public Task<bool> SendMail(Email email);
}