using AutoMapper;
using BooksStorage.DTOs;
using BooksStorage.Models;
using BooksStorage.Models.Mail;

namespace BooksStorage.Profiles;

public class EmailNewsletterProfile : Profile
{
    public EmailNewsletterProfile()
    {
        CreateMap<EmailNewsletterCreateDTO, EmailNewsletter>();
    }
}