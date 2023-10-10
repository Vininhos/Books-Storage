using BooksStorage.Data;
using BooksStorage.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<BookStorageDatabaseSettings>(builder.Configuration.GetSection("BookStorageSettings"));
builder.Services.AddSingleton<IBookRepository, BookRepository>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(opt => { opt.UseInMemoryDatabase("InMem"); });
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddCors(options =>
{
    options.AddPolicy("Access-Control-Allow-Origin",
        corsPolicyBuilder =>
        {
            corsPolicyBuilder.WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

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