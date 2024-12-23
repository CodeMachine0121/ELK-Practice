using Elastic.Apm.DiagnosticSource;
using Elastic.Apm.SerilogEnricher;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using ILogger = Microsoft.Extensions.Logging.ILogger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddElasticApm(new HttpDiagnosticsSubscriber());
Log.Logger = new LoggerConfiguration()
        .Enrich.WithElasticApmCorrelationInfo()
        .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
        {
            AutoRegisterTemplate = true, IndexFormat = "apm-logs-{0:yyyy.MM.dd}"
        })
        .CreateLogger();
builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();


app.Run();
