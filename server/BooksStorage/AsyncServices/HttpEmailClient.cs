namespace BooksStorage.AsyncServices;

public class HttpEmailClient : IHttpEmailClient
{
    private HttpClient _client = new();

    public Task SendMailRequest(Mail.Models.Mail mail)
    {
        //HttpResponseMessage response = await _client.PostAsJsonAsync("");
        //TO DO
    }
}