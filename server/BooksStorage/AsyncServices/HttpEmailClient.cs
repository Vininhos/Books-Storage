using System.Text;
using System.Text.Json;

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

    public async Task SendMailRequest(Mail.Models.Mail mail)
    {
        try
        {
            var httpContent = new StringContent(JsonSerializer.Serialize(mail), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(_configuration["MailerService"], httpContent);

            if (response.IsSuccessStatusCode)
                Console.WriteLine("--> Sync POST to MailerService.");
            else
                Console.WriteLine("--> Sync POST to MailerService failed.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("--> Failed to send request to Mailer Service. Error: {0}", ex);
        }
    }
}