using Books_Storage.Models;
using Microsoft.EntityFrameworkCore;

namespace Books_Storage.Data;

public class AppDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
    {
    }
}