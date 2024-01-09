using System.Net.Mail;
using BooksStorage.Mail.Data;
using BooksStorage.Mail.Models;
using Microsoft.AspNetCore.Mvc;

namespace BooksStorage.Mail.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class MailController : ControllerBase
{
    private readonly IMailRepository _mailRepository;

    public MailController(IMailRepository mailRepository)
    {
        _mailRepository = mailRepository;
    }

    [HttpPost(Name = "SendMail")]
    public ActionResult SendMail(Email email)
    {
        var emailSent = _mailRepository.SendMail(email);
        if (emailSent.IsCompletedSuccessfully)
            return Ok("Email was sent.");
        
        return BadRequest("Failed sending e-mail");
    }
}