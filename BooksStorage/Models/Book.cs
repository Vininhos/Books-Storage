using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BooksStorage.Models;

public class Book
{
    [BsonId]
    public ObjectId Id { get; set; }

    [Required] public string Name { get; set; }
    [Required] public string Author { get; set; }

    [Required]
    [Display(Name = "Year of Publish")]
    public int PublishYear { get; set; }

    [Required] public decimal Price { get; set; }
    [Required] public string Category { get; set; }
}