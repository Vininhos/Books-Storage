using AutoMapper;
using BooksStorage.DTOs;
using BooksStorage.DTOs.Book;
using BooksStorage.Models;
using BooksStorage.Models.Book;

namespace BooksStorage.Profiles;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<BookCreateDto, Book>();
        CreateMap<Book, BookReadDto>();
    }
}