using BooksStorage.Data;
using BooksStorage.Models;
using Microsoft.AspNetCore.Mvc;

namespace BooksStorage.Controllers;

[ApiController]
[Route("/api/[controller]", Name = "BookController")]
public class BookController : ControllerBase
{
    private readonly IBookRepository _bookRepository;

    public BookController(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    [HttpGet(Name = "Get All Books")]
    public ActionResult<IEnumerable<Book>> GetAllBooks()
    {
        Console.WriteLine("--> Getting All Books...");

        var books = _bookRepository.GetAllBooks();

        return Ok(books);
    }

    [HttpPost(Name = "Insert a Book")]
    public ActionResult<Book> InsertBook(Book book)
    {
        Console.WriteLine("--> Inserting a book...");

        _bookRepository.InsertBook(book);

        return CreatedAtAction(nameof(InsertBook), new { id = book.Id }, book);
    }
}