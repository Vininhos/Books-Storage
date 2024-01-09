using AutoMapper;
using BooksStorage.Data;
using BooksStorage.DTOs.Book;
using BooksStorage.Models.Book;
using Microsoft.AspNetCore.Mvc;

namespace BooksStorage.Controllers;

[ApiController]
[Route("/api/[controller]", Name = "BookController")]
public class BookController : ControllerBase
{
    private readonly IBookRepository _bookRepository;
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public BookController(IBookRepository bookRepository, AppDbContext context, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _context = context;
        _mapper = mapper;
    }

    [HttpGet(Name = "Get All Books")]
    public async Task<ActionResult<IEnumerable<BookReadDto>>> GetAllBooks()
    {
        Console.WriteLine("--> Getting All Books...");

        var books = await _bookRepository.GetAllBooksAsync();

        return Ok(_mapper.Map<IEnumerable<BookReadDto>>(books));
    }

    [HttpPost(Name = "Insert a Book")]
    public async Task<ActionResult<Book>> InsertBook(BookCreateDto bookCreateDto)
    {
        Console.WriteLine("--> Inserting a book...");

        var book = _mapper.Map<Book>(bookCreateDto);

        await _bookRepository.InsertBookAsync(book);

        return CreatedAtAction(nameof(InsertBook), new { id = book.Id }, book);
    }
    
    [HttpPost("/api/Book/email",Name = "Insert a Book and Send Email")]
    public async Task<ActionResult<Book>> InsertBookAndSendEmail(BookCreateDto bookCreateDto, string email)
    {
        Console.WriteLine("--> Inserting a book...");

        var book = _mapper.Map<Book>(bookCreateDto);

        await _bookRepository.InsertBookAsync(book);

        Console.WriteLine("Sending e-mail...");

        await _bookRepository.SendEmailRequest(email, book);

        return CreatedAtAction(nameof(InsertBook), new { id = book.Id }, book);
    }

    [HttpGet("{id}", Name = "Get book by Id")]
    public async Task<ActionResult<Book>> GetBookByIdAsync(string id)
    {
        Console.WriteLine("--> Getting book by id...");

        var book = await _bookRepository.GetBookByIdAsync(id);

        if (book is null)
            return NotFound("The book was not found or doesn't exist.");

        return Ok(_mapper.Map<BookReadDto>(book));
    }
}