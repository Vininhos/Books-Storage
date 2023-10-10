namespace BooksStorage.Mail.Models;

public class Message
{
    public string From { get; set; }
    public string FromName { get; set; }
    public string To { get; set; }
    public string Subject { get; set; }
    public string[] Attachments { get; set; }
}