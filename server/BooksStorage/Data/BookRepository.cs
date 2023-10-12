using BooksStorage.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BooksStorage.Data;

public class BookRepository : IBookRepository
{
    private readonly IMongoCollection<Book> _bookCollection;

    public BookRepository(IOptions<BookStorageDatabaseSettings> bookStorageDatabaseSettings)
    {
        var mongoClient = new MongoClient(bookStorageDatabaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(bookStorageDatabaseSettings.Value.DatabaseName);

        _bookCollection = mongoDatabase.GetCollection<Book>(bookStorageDatabaseSettings.Value.BookCollectionName);
    }

    public async Task<List<Book>> GetAllBooksAsync() =>
        await _bookCollection.Find(_ => true).ToListAsync();

    public async Task<Book> GetBookByIdAsync(string id) =>
        await _bookCollection.Find(b => b.Id.ToString() == id).FirstOrDefaultAsync();

    public async Task InsertBookAsync(Book book) =>
        await _bookCollection.InsertOneAsync(book);
}