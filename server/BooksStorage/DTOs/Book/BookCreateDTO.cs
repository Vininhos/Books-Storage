using System.ComponentModel.DataAnnotations;

namespace BooksStorage.DTOs;

public class BookCreateDTO
{
    [Required] public string Name { get; set; }
    [Required] public string Author { get; set; }

    [Required]
    [Display(Name = "Year of Publish")]
    public int PublishYear { get; set; }

    [Required] public decimal Price { get; set; }
    [Required] public string Category { get; set; }
}