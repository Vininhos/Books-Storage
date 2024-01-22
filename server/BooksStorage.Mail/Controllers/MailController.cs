using BooksStorage.Mail.Data;
using BooksStorage.Mail.Models;
using Microsoft.AspNetCore.Mvc;

namespace BooksStorage.Mail.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class MailController : ControllerBase
{
  private readonly IMailRepository _mailRepository;
  private readonly ILogger<MailController> _logger;

  public MailController(IMailRepository mailRepository, ILogger<MailController> logger)
  {
    _mailRepository = mailRepository;
    _logger = logger;
  }

  [HttpPost(Name = "SendMail")]
  public ActionResult SendMail(Email email)
  {
    var emailSent = _mailRepository.SendMail(email);

    if (emailSent.IsCompletedSuccessfully)
      return Ok("Email was sent.");

    return BadRequest("Failed sending e-mail. Try sending it later.");
  }
}
