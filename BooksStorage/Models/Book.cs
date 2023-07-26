using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BooksStorage.Models;

public class Book
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Author { get; set; }
    public int PublishYear { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
}