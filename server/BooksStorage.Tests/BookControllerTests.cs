using AutoMapper;
using BooksStorage.Controllers;
using BooksStorage.Data;
using BooksStorage.DTOs;
using BooksStorage.DTOs.Book;
using BooksStorage.Models;
using BooksStorage.Models.Book;
using BooksStorage.Profiles;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using Moq;

namespace BooksStorage.Tests;

public class BookControllerTests
{
    private readonly Mock<IBookRepository> _repositoryStub;
    private readonly BookProfile _bookProfile;
    private MapperConfiguration _configuration;
    private IMapper _mapper;
    private Mock<ILogger<BookController>> _bookLogger;

    public BookControllerTests()
    {
        _repositoryStub = new Mock<IBookRepository>();
        _bookProfile = new BookProfile();
        _configuration = new MapperConfiguration(config => config.AddProfile(_bookProfile));
        _mapper = _configuration.CreateMapper();
        _bookLogger = new Mock<ILogger<BookController>>();
    }

    [Fact]
    public async Task Test_GetBookByIdAsync_ReturnsNotFoundObjectResult()
    {
        // Arrange
        _repositoryStub.Setup(repo => repo.GetBookByIdAsync(It.IsAny<string>())).ReturnsAsync((Book)null);
        
        BookController controller = new BookController(_repositoryStub.Object, null, _mapper, _bookLogger.Object);
        // Act
        ActionResult<Book> result = await controller.GetBookByIdAsync(ObjectId.GenerateNewId().ToString());

        // Assert
        result.Result.Should().BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public async Task Test_GetBookByIdAsync_WithExistingBook_ReturnsExpectedBook()
    {
        // Arrange
        var expectedBook = CreateTestBook();

        _repositoryStub.Setup(repo => repo.GetBookByIdAsync(It.IsAny<string>())).ReturnsAsync(expectedBook);

        var controller = new BookController(_repositoryStub.Object, null, _mapper, _bookLogger.Object);
        // Act
        var result = await controller.GetBookByIdAsync(ObjectId.GenerateNewId().ToString());

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);

        var bookResult = Assert.IsType<BookReadDto>(okResult.Value);

        bookResult.Should().BeEquivalentTo(expectedBook,
            options => options.Excluding(b => b.Id).Excluding(b => b.ViewCount).ComparingByMembers<BookReadDto>());
    }

    [Fact]
    public async Task Test_GetBooksAsync_WithExistingBooks_ReturnsAllBooks()
    {
        // Arrange
        var expectedBooks = CreateTestBooks();

        _repositoryStub.Setup(repo => repo.GetAllBooksAsync()).ReturnsAsync(expectedBooks);

        var controller = new BookController(_repositoryStub.Object, null, _mapper, _bookLogger.Object);
        // Act
        var result = await controller.GetAllBooks();

        //Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);

        var booksResult = Assert.IsAssignableFrom<IEnumerable<BookReadDto>>(okResult.Value);

        booksResult.Should().BeEquivalentTo(expectedBooks,
            options => options.Excluding(b => b.Id).Excluding(b => b.ViewCount).ComparingByMembers<BookReadDto>());
    }

    private Book CreateTestBook()
    {
        Book book = new()
        {
            Id = ObjectId.GenerateNewId(),
            Name = "Python for Data Science", Author = "Wilson Robert", Category = "Programming",
            PublicationYear = 2020, Price = 999
        };

        return book;
    }

    private List<Book> CreateTestBooks()
    {
        List<Book> books = new List<Book>
        {
            new()
            {
                Id = ObjectId.GenerateNewId(),
                Name = "Python for Data Science", Author = "Wilson Robert", Category = "Programming",
                PublicationYear = 2020, Price = 999
            },
            new()
            {
                Id = ObjectId.GenerateNewId(),
                Name = "Java for Beginners", Author = "Yan Moraes", Category = "Programming", PublicationYear = 2022,
                Price = 5499
            },
            new()
            {
                Id = ObjectId.GenerateNewId(),
                Name = "Falling in Love", Author = "Matheus Nobrega", Category = "Drama", PublicationYear = 2015,
                Price = 199
            }
        };
        
        return books;
    }
}