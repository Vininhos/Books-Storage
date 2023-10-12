using BooksStorage.Models;
using MongoDB.Driver;

namespace BooksStorage.Data;

public interface IBookRepository
{
    Task<List<Book>> GetAllBooksAsync();
    Task<Book> GetBookByIdAsync(string id);
    Task InsertBookAsync(Book book);
    Task<UpdateResult> UpdateBookCounter(Book book);
}