using Books_Storage.Models;

namespace Books_Storage.Data;

public class BookRepository : IBookRepository
{
    private readonly AppDbContext _context;

    public BookRepository(AppDbContext context)
    {
        _context = context;
    }

    public bool SaveChanges()
    {
        return (_context.SaveChanges() > 0);
    }

    public IEnumerable<Book> GetAllBooks()
    {
        return _context.Books.ToList();
    }

    public void CreateBook(Book book)
    {
        if (book == null)
            throw new ArgumentException("Book cannot be null.");

        _context.Books.Add(book);
    }

    public void UpdateBook(Book book)
    {
        if (book == null)
            throw new ArgumentException("Book cannot be null.");

        _context.Remove(_context.Books.FirstOrDefault(b => b.Id == book.Id));

        _context.Books.Add(book);
    }

    public void DeleteBook(int id)
    {
        var book = _context.Books.FirstOrDefault(b => b.Id == id);
        if (book == null)
            throw new ArgumentException("Book doesn't exist.");
        
        _context.Books.Remove(_context.Books.FirstOrDefault(b => b.Id == id));
    }
}