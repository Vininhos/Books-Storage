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

        _bookCollection = mongoDatabase.GetCollection<Book>(bookStorageDatabaseSettings.Value.BooksCollectionName);
    }

    public List<Book> GetAllBooks()
    {
        return _bookCollection.Find(_ => true).ToList();
    }

    public void InsertBook(Book book)
    {
        if (book is null)
            throw new ArgumentException("Book cannot be null.");

        _bookCollection.InsertOne(book);
    }
}