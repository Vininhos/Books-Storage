using BooksStorage.AsyncServices;
using BooksStorage.Data;
using BooksStorage.Models.Book;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<BookStorageDatabaseSettings>(builder.Configuration.GetSection("BookStorageSettings"));
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IEmailNewsletterRepository, EmailNewsletterRepository>();
builder.Services.AddScoped<IHttpEmailClient, HttpEmailClient>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(opt => { opt.UseInMemoryDatabase("InMem"); });
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    PrepDb.PrepPopulation(app);
}

//app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("Access-Control-Allow-Origin");

app.UseAuthorization();

app.MapControllers();

app.Run();