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
    public async Task<ActionResult<IEnumerable<Book>>> GetAllBooks()
    {
        Console.WriteLine("--> Getting All Books...");

        var books = await _bookRepository.GetAllBooksAsync();

        return Ok(books);
    }

    [HttpPost(Name = "Insert a Book")]
    public async Task<ActionResult<Book>> InsertBook(Book book)
    {
        Console.WriteLine("--> Inserting a book...");

        await _bookRepository.InsertBookAsync(book);

        return CreatedAtAction(nameof(InsertBook), new { id = book.Id }, book);
    }

    [HttpGet("{id}", Name = "Get book by Id")]
    public async Task<ActionResult<Book>> GetBookByIdAsync(string id)
    {
        Console.WriteLine("--> Getting book by id...");

        var book = await _bookRepository.GetBookByIdAsync(id);

        if (book is null)
            return NotFound("The book was not found or doesn't exist.");
            

        return Ok(book);
    }
}