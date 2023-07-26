using BooksStorage.Models;

namespace BooksStorage.Data;

public interface IBookRepository
{
    List<Book> GetAllBooks();
    void InsertBook(Book book);
}