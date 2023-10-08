using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;

namespace BooksStorage.Mail.Controllers;

[ApiController]
[Route("Controller")]
public class MailController : ControllerBase
{
    [HttpPost(Name = "SendMail")]
    public string SendMail()
    {
        //TO DO smtpClient
        return null;
    }
}