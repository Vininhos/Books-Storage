using BooksStorage.Controllers;
using BooksStorage.Data;
using BooksStorage.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Moq;

namespace BooksStorage.Tests;

public class BookControllerTests
{
    private readonly Mock<IBookRepository> _repositoryStub = new();

    [Fact]
    public async Task Test_GetBookByIdAsync_ReturnsNotFoundObjectResult()
    {
        // Arrange
        _repositoryStub.Setup(repo => repo.GetBookByIdAsync(It.IsAny<string>())).ReturnsAsync((Book)null);

        BookController controller = new BookController(_repositoryStub.Object, null);
        // Act
        ActionResult<Book> result = await controller.GetBookByIdAsync(ObjectId.GenerateNewId().ToString());

        // Assert
        result.Result.Should().BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public async Task Test_GetBookByIdAsync_WithExistingBook_ReturnsExpectedBook()
    {
        // Arrange
        Book expectedBook = GetTestBook();

        _repositoryStub.Setup(repo => repo.GetBookByIdAsync(It.IsAny<string>())).ReturnsAsync(expectedBook);

        BookController controller = new BookController(_repositoryStub.Object, null);
        
        // Act
        ActionResult<Book> result = await controller.GetBookByIdAsync(ObjectId.GenerateNewId().ToString());

        // Assert
        result.Value.Should().BeEquivalentTo(expectedBook);
    }

    private Book GetTestBook()
    {
        Book book = new Book()
        {
            Id = ObjectId.GenerateNewId(),
            Name = "Python for Data Science", Author = "Wilson Robert", Category = "Programming",
            PublishYear = 2020, Price = 999
        };

        return book;
    }
}