using AutoMapper;
using BooksStorage.DTOs;
using BooksStorage.Models;

namespace BooksStorage.Profiles;

public class EmailNewsletterProfile : Profile
{
    public EmailNewsletterProfile()
    {
        CreateMap<EmailNewsletterCreateDTO, EmailNewsletter>();
    }
}