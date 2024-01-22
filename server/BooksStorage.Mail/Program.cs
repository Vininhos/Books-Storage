using BooksStorage.Mail.Data;
using BooksStorage.Mail.Models;
using Serilog;
using Serilog.Sinks.Elasticsearch;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<MailHogSettings>(builder.Configuration.GetSection("MailHog"));
builder.Services.AddScoped<IMailRepository, MailRepository>();

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
