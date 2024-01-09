namespace BooksStorage.DTOs.Book;

public class BookReadDto
{
    public string Name { get; set; }
    public string Author { get; set; }
    public int PublicationYear { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
}