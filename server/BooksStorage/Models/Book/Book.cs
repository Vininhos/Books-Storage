using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BooksStorage.Models.Book;

public class Book
{
    [BsonId] public ObjectId Id { get; set; }

    [Required] public string Name { get; set; }
    [Required] public string Author { get; set; }

    [Required]
    [Display(Name = "Year of Publication")]
    public int PublicationYear { get; set; }

    [Required] public decimal Price { get; set; }
    [Required] public string Category { get; set; }

    public long ViewCount { get; set; } = 0;
}