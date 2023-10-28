using AutoMapper;
using BooksStorage.Data;
using BooksStorage.DTOs;
using BooksStorage.Models;
using BooksStorage.Models.Mail;
using Microsoft.AspNetCore.Mvc;

namespace BooksStorage.Controllers;

[ApiController]
[Route("/api/[controller]", Name = "NewsletterController")]
public class EmailNewsletterController : ControllerBase
{
    private readonly IEmailNewsletterRepository _emailNewsletterRepository;
    private readonly IMapper _mapper;

    public EmailNewsletterController(IEmailNewsletterRepository emailNewsletterRepository, IMapper mapper)
    {
        _emailNewsletterRepository = emailNewsletterRepository;
        _mapper = mapper;
    }

    [HttpPost(Name = "Register to Newsletter")]
    public async Task<ActionResult<EmailNewsletter>> Register(EmailNewsletterCreateDTO emailNewsletterCreateDto)
    {
        Console.WriteLine("--> Registering email {0}...", emailNewsletterCreateDto.EmailAddress);

        var emailNewsletter = _mapper.Map<EmailNewsletter>(emailNewsletterCreateDto);

        await _emailNewsletterRepository.Register(emailNewsletter);

        return CreatedAtAction(nameof(Register), new { id = emailNewsletter.Id }, emailNewsletter);
    }
}