using System.Text;
using System.Text.Json;
using BooksStorage.Models.Mail;

namespace BooksStorage.AsyncServices;

public class HttpEmailClient : IHttpEmailClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public HttpEmailClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task SendMailRequest(Email email)
    {
        try
        {
            var httpContent = new StringContent(JsonSerializer.Serialize(email), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(_configuration["MailerService"], httpContent);

            Console.WriteLine(response.IsSuccessStatusCode
                ? "--> Sync POST to MailerService."
                : "--> Sync POST to MailerService failed.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("--> Failed to send request to Mailer Service. Error: {0}", ex);
        }
    }
}