#region Logging

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

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddModularServices(builder);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

try
{
    string rootFolder = builder.Configuration.GetSection(Directory.GetCurrentDirectory()).Value!;
    string qr = builder.Configuration.GetSection("Qr").Value!;
    string profile = builder.Configuration.GetSection("Profile").Value!;
    string venue = builder.Configuration.GetSection("Venue").Value!;

    app.UseLogicalFileService(rootFolder, qr);
    app.UseLogicalFileService(rootFolder, profile);
    app.UseLogicalFileService(rootFolder, venue);
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}

app.UseAuthorization();

app.MapControllers();

app.Run();