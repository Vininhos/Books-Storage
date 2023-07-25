using Books_Storage.Models;
using Microsoft.AspNetCore.Mvc;

namespace Books_Storage.Controllers;

[ApiController]
[Route("/api/[controller]", Name = "BookController")]
public class BookController : ControllerBase
{
   public BookController()
   {
      
   }
   
   [HttpGet(Name = "Get All Books")]
   public Book GetAllBooks()
   {
       return null;
   }
}