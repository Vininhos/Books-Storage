using System.Net.Mail;
using System.Text.Json;
using BooksStorage.Mail.Models;
using Microsoft.AspNetCore.Mvc;

namespace BooksStorage.Mail.Controllers;

[ApiController]
[Route("Controller")]
public class MailController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public MailController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    [HttpPost(Name = "SendMail")]
    public ActionResult SendMail(string mailMessageRequest)
    {
        Email email = JsonSerializer.Deserialize<Email>(mailMessageRequest);
        MailMessage m = new MailMessage("dontreply@bookstorage.com", "hi@mom.com");
        m.Subject = "Thanks for your help!";
        m.Body = "Thanks for adding a book to our website! We're glad to have you as a contributor.";
        SmtpClient smtpClient = new SmtpClient("localhost", 1025);
        try
        {
            smtpClient.Send(m);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
        
        return Ok("Email was sent.");
    }
}