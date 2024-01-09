using BooksStorage.Models.Book;
using MongoDB.Bson;

namespace BooksStorage.Data;

public static class PrepDb
{
    public static void PrepPopulation(IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
        }
    }

    private static void SeedData(AppDbContext context)
    {
        Console.WriteLine("Seeding data...");

        context.Books.AddRange(
            new Book()
            {
                Id = ObjectId.GenerateNewId(),
                Name = "Python for Data Science", Author = "Wilson Robert", Category = "Programming",
                PublicationYear = 2020, Price = 999
            },
            new Book()
            {
                Id = ObjectId.GenerateNewId(),
                Name = "Java for Beginners", Author = "Yan Moraes", Category = "Programming", PublicationYear = 2022,
                Price = 5499
            },
            new Book()
            {
                Id = ObjectId.GenerateNewId(),
                Name = "Falling in Love", Author = "Matheus Nobrega", Category = "Drama", PublicationYear = 2015,
                Price = 199
            }
        );

        context.SaveChanges();
    }
}