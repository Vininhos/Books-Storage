using Books_Storage.Models;

namespace Books_Storage.Data;

public interface IBookRepository
{
    bool SaveChanges();
    
   IEnumerable<Book> GetAllBooks();
   void CreateBook(Book book);
   void UpdateBook(Book book);
   void DeleteBook(int id);
}