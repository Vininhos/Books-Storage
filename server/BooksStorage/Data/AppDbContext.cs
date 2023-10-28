using BooksStorage.Models;
using BooksStorage.Models.Book;
using Microsoft.EntityFrameworkCore;

namespace BooksStorage.Data;

public class AppDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
    {
    }
}