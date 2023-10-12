using BooksStorage.Models;

namespace BooksStorage.Data;

public interface IEmailNewsletterRepository
{
    public Task Register(EmailNewsletter email);
}