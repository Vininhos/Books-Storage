using System.Text;
using System.Text.Json;
using BooksStorage.Controllers;
using BooksStorage.Models.Mail;
using Microsoft.AspNetCore.Mvc;

namespace BooksStorage.AsyncServices;

public class HttpEmailClient : IHttpEmailClient
{
  private readonly HttpClient _httpClient;
  private readonly IConfiguration _configuration;
  private readonly ILogger<BookController> _logger;

  public HttpEmailClient(IConfiguration configuration, ILogger<BookController> logger)
  {
    _httpClient = new HttpClient();
    _configuration = configuration;
    _logger = logger;
  }

  [HttpPost]
  public async Task SendMailRequest(Email email)
  {
    try
    {
      var httpContent = new StringContent(JsonSerializer.Serialize(email), Encoding.UTF8, "application/json");
      HttpResponseMessage response = await _httpClient.PostAsync(_configuration["MailerService"], httpContent);

      if (response.IsSuccessStatusCode)
        _logger.LogInformation("E-mail was sent successfully.");

      else
        _logger.LogError("Email sent attempt failed.");
    }
    catch (Exception ex)
    {
      _logger.LogError("Failed to send request to Mailer Service. Error: {Ex}", ex);
    }
  }
}
