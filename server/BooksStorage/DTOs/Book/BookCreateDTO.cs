using System.ComponentModel.DataAnnotations;

namespace BooksStorage.DTOs;

public class BookCreateDTO
{
    [Required] public string Name { get; set; }
    [Required] public string Author { get; set; }

    [Required]
    [Display(Name = "Year of Publication")]
    public int PublicationYear { get; set; }

    [Required] public decimal Price { get; set; }
    [Required] public string Category { get; set; }
}