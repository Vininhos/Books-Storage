using BooksStorage.Models;
using BooksStorage.Models.Mail;

namespace BooksStorage.Data;

public interface IEmailNewsletterRepository
{
    public Task Register(EmailNewsletter email);
}