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

builder.Host.UseSerilog((context, configuration) =>
    {
      configuration.Enrich.FromLogContext()
      .Enrich.WithMachineName()
      .WriteTo.Console()
      .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(context.Configuration["ElasticConfiguration:Uri"]))
      {
        IndexFormat = context.Configuration["ElasticConfiguration:Index"],
        AutoRegisterTemplate = true,
        NumberOfShards = int.Parse(context.Configuration["ElasticConfiguration:NumberOfShards"]),
        NumberOfReplicas = int.Parse(context.Configuration["ElasticConfiguration:NumberOfReplicas"]),
        ModifyConnectionSettings = x => x.BasicAuthentication(context.Configuration["ElasticConfiguration:User"], context.Configuration["ElasticConfiguration:Password"])
      })
      .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
      .ReadFrom.Configuration(context.Configuration);
    });


builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowOrigin",
      corsPolicyBuilder => corsPolicyBuilder.AllowAnyOrigin()
          .AllowAnyHeader()
          .AllowAnyMethod());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  PrepDb.PrepPopulation(app);
}

app.UseSwagger();

app.UseSwaggerUI();

//app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();
