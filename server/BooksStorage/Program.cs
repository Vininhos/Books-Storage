using BooksStorage.AsyncServices;
using BooksStorage.Data;
using BooksStorage.Models.Book;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Sinks.Elasticsearch;

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

var elasticUrl = builder.Configuration.GetValue<string>("ElasticConfiguration:URL");
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUrl))
    {
        AutoRegisterTemplate = true
    })
    .CreateLogger();

builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowOrigin",
      corsPolicyBuilder => corsPolicyBuilder.AllowAnyOrigin()
          .AllowAnyHeader()
          .AllowAnyMethod());
});

builder.Services.AddLogging(options =>
{
  options.AddSimpleConsole(c =>
  {
    c.TimestampFormat = "[dd-MM-yyyy HH:mm:ss] ";
  });
});

var app = builder.Build();
var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  PrepDb.PrepPopulation(app);
}

loggerFactory.AddSerilog();

app.UseSwagger();

app.UseSwaggerUI();

//app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();
