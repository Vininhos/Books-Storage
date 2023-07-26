using BooksStorage.Models;

namespace BooksStorage.Data;

public interface IBookRepository
{
    bool SaveChanges();
    
   IEnumerable<Book> GetAllBooks();
   void CreateBook(Book book);
   void UpdateBook(Book book);
   void DeleteBook(int id);
}