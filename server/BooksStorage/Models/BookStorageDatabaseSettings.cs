namespace BooksStorage.Models;

public class BookStorageDatabaseSettings
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
    public string BookCollectionName { get; set; }
    public string EmailNewsletterCollectionName { get; set; }
}