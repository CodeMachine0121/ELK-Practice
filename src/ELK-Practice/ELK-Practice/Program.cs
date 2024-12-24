using Elastic.Apm.DiagnosticSource;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddElasticApm(new HttpDiagnosticsSubscriber());
var logger = new LoggerConfiguration()
    .WriteTo.File(
        $"../logs/{DateTime.Now:yyyy-MM-dd}.DEBUG.log",
        LogEventLevel.Debug)
    .WriteTo.File(
        $"../logs/{DateTime.Now:yyyy-MM-dd}.Error.log", // 用於 Error 級別的檔案
        LogEventLevel.Error)
    .CreateLogger();

Log.Logger = logger;
builder.Host.UseSerilog();

var timer = new Timer(e =>
{
    Log.Information("Information log");
    Log.Warning("Warning log");
    Log.Error("Error log");
    Log.Fatal("Fatal log");
    Log.Debug("Debug log");
    Log.Verbose("Verbose log");
    Console.WriteLine("write log");

}, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));


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