using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BooksStorage.Models.Mail;

public class EmailNewsletter
{
    [BsonId] public ObjectId Id { get; set; }
    [Required] public string EmailAddress { get; set; }
}