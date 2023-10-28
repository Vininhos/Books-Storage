using BooksStorage.AsyncServices;
using BooksStorage.Models.Book;
using BooksStorage.Models.Mail;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BooksStorage.Data;

public class BookRepository : IBookRepository
{
    private readonly IMongoCollection<Book> _bookCollection;
    private readonly IHttpEmailClient _httpEmailClient;
    private readonly IConfiguration _configuration;

    public BookRepository(IOptions<BookStorageDatabaseSettings> bookStorageDatabaseSettings, IHttpEmailClient httpEmailClient, IConfiguration configuration)
    {
        var mongoClient = new MongoClient(bookStorageDatabaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(bookStorageDatabaseSettings.Value.DatabaseName);
        _httpEmailClient = httpEmailClient;
        _configuration = configuration;
        
        _bookCollection = mongoDatabase.GetCollection<Book>(bookStorageDatabaseSettings.Value.BookCollectionName);
    }

    public async Task<List<Book>> GetAllBooksAsync() =>
        await _bookCollection.Find(_ => true).ToListAsync();

    public async Task<Book> GetBookByIdAsync(string id)
    {
        var book = await _bookCollection.Find(b => b.Id.ToString() == id).FirstOrDefaultAsync();

        if (book is null)
            return null;

        var result = await UpdateBookCounter(book);
        
        return book;
    }

    public async Task InsertBookAsync(Book book) =>
        await _bookCollection.InsertOneAsync(book);

    public async Task<UpdateResult> UpdateBookCounter(Book book)
    {
        var update = Builders<Book>.Update
            .Set(b => b.ViewCount, ++book.ViewCount);

        return await _bookCollection.UpdateOneAsync(b => b.Id == book.Id, update);
    }

    public async Task SendEmailRequest(string emailAddress, Book book)
    {
        Email email = new()
        {
            From = _configuration["BooksStorageDefaultMailAddress"],
            To = emailAddress,
            Subject = "Success adding a book",
            Body = $"Thanks for adding {book.Name}!"
        };

        await _httpEmailClient.SendMailRequest(email);
    }
}