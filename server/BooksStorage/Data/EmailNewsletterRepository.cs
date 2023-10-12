using BooksStorage.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BooksStorage.Data;

public class EmailNewsletterRepository : IEmailNewsletterRepository
{
    private readonly IMongoCollection<EmailNewsletter> _emailNewsletter;

    public EmailNewsletterRepository(IOptions<BookStorageDatabaseSettings> databaseSettings)
    {
        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

        _emailNewsletter =
            mongoDatabase.GetCollection<EmailNewsletter>(databaseSettings.Value.EmailNewsletterCollectionName);
    }

    public async Task Register(EmailNewsletter email) =>
        await _emailNewsletter.InsertOneAsync(email);
}