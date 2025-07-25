#region Logging
using System.Net;
using System.Net.Mail;


string logFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
string logFileName = Path.Combine(logFolderPath, "logs_");

if (!Directory.Exists(logFolderPath))
{
    Directory.CreateDirectory(logFolderPath);
}

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(
        path: Path.Combine(logFileName),
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
        rollingInterval: RollingInterval.Hour)
    .CreateLogger();

#endregion

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSerilog();

builder.Services
    .AddFluentEmail("eventticketingsystem.opom@gmail.com")
    .AddSmtpSender(new SmtpClient("smtp.gmail.com")
    {
        UseDefaultCredentials = false,
        Credentials = new NetworkCredential(
                "eventticketingsystem.opom@gmail.com",
                "qpqo kczf gffk bycz"),
        EnableSsl = true,
        Port = 587,
        DeliveryMethod = SmtpDeliveryMethod.Network,
        Timeout = 10000
    });
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddModularServices(builder);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();