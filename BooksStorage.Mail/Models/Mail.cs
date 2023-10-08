namespace BooksStorage.Mail.Models;

public class Mail
{
    public string Domain;
    public string Host;
    public int Port { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Encryption { get; set; }
    public string FromAddress { get; set; }
    public string FromName { get; set; }
}