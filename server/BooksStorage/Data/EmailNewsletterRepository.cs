using BooksStorage.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BooksStorage.Data;

public class EmailNewsletterRepository : IEmailNewsletterRepository
{
    private readonly IMongoCollection<EmailNewsletter> _emailNewsletter;

    public EmailNewsletterRepository(IOptions<EmailNewsletterDatabaseSettings> emailNewsletterDatabaseSettings)
    {
        var mongoClient = new MongoClient(emailNewsletterDatabaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(emailNewsletterDatabaseSettings.Value.DatabaseName);

        _emailNewsletter =
            mongoDatabase.GetCollection<EmailNewsletter>(emailNewsletterDatabaseSettings.Value.CollectionName);
    }

    public async Task Register(EmailNewsletter email) => 
        await _emailNewsletter.InsertOneAsync(email);
}