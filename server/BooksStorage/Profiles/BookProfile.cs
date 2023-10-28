using AutoMapper;
using BooksStorage.DTOs;
using BooksStorage.Models;
using BooksStorage.Models.Book;

namespace BooksStorage.Profiles;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<BookCreateDTO, Book>();
        CreateMap<Book, BookReadDTO>();
    }
}