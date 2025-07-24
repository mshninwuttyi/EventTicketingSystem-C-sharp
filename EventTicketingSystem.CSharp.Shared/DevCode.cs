namespace EventTicketingSystem.CSharp.Shared;

public static partial class DevCode
{
    private const long MaxFileSize = 5 * 1024 * 1024;

    #region Extensions

    public static string ToJson(this object obj)
    {
        return JsonConvert.SerializeObject(obj);
    }

    public static T ToObject<T>(this string jsonStr)
    {
        try
        {
            var result = JsonConvert.DeserializeObject<T>(jsonStr,
                new JsonSerializerSettings { DateParseHandling = DateParseHandling.DateTimeOffset });
            return result!;
        }
        catch
        {
            return (T)Convert.ChangeType(jsonStr, typeof(T));
        }
    }

    public static string GetEnumDescription(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());

        var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
        return attribute?.Description ?? value.ToString();
    }

    public static string GenerateUlid()
    {
        return Ulid.NewUlid().ToString();
    }

    #endregion

    #region Validation

    public static bool IsNullOrEmpty(this object? str)
    {
        var result = true;
        try
        {
            result = str == null ||
                     string.IsNullOrEmpty(str.ToString()?.Trim()) ||
                     string.IsNullOrWhiteSpace(str.ToString()?.Trim());
        }
        catch
        {
            // ignored
        }

        return result;
    }

    public static bool IsValidEmail(this string email)
    {
        bool result = true;

        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            if (addr.Address != email)
            {
                result = false;
            }
        }
        catch
        {
            result = false;
        }

        return result;
    }


    #endregion

    #region Thousand Separators

    public static string ToThousandSeparator<T>(this T value) where T : struct, IFormattable
    {
        return value.ToString("N", null); // 1,000,000
    }

    #endregion

    #region Date Format MM Version

    public static string ToDateTimeFormat(this DateTime date)
    {
        return date.ToString("dd-MM-yyyy"); // 08-07-2025
    }

    public static string ToDateTimeFormat(this DateTime? date)
    {
        return date.HasValue ? date.Value.ToDateTimeFormat() : string.Empty;
    }

    public static string ToDateTimeFormatLong(this DateTime date)
    {
        return date.ToString("dd-MMMM-yyyy"); // 08-July-2025
    }

    #endregion

    #region Custom Log Errors

    public static void LogExceptionError(this ILogger logger,
        Exception ex,
        [CallerFilePath] string filePath = "",
        [CallerMemberName] string methodName = "")
    {
        var fileName = Path.GetFileName(filePath);
        var message = $"File Name - {fileName} | Method Name - {methodName} | Error - {ex.ToJson()}";
        logger.LogCustomError(message);
    }

    public static void LogCustomError(this ILogger logger,
        object? str,
        [CallerFilePath] string filePath = "",
        [CallerMemberName] string methodName = "")
    {
        var fileName = Path.GetFileName(filePath);
        var message =
            $"File Name - {fileName} | Method Name - {methodName} | Result - {(str is string ? str : str.ToJson())}";
        logger.LogError(message);
    }

    public static void LogCustomInformation(this ILogger logger,
        object str,
        [CallerFilePath] string filePath = "",
        [CallerMemberName] string methodName = "")
    {
        var fileName = Path.GetFileName(filePath);
        var message = $"File Name - {fileName} | Method Name - {methodName} | Result - {(str is string ? str : str.ToJson())}";
        logger.LogInformation(message);
    }

    #endregion

    #region DbContext Extension

    public static async Task<int> SaveAndDetachAsync(this DbContext db)
    {
        var res = await db.SaveChangesAsync();
        foreach (var entry in db.ChangeTracker.Entries().ToArray())
        {
            entry.State = EntityState.Detached;
        }

        return res;
    }

    #endregion

    #region File Upload

    public static async Task<FileUploadData> UploadFileAsync(IFormFile file, EnumDirectory directory)
    {
        if (file is null || file.Length is 0)
        {
            throw new Exception("Error: No file was uploaded.");
        }

        if (file.Length > MaxFileSize)
        {
            throw new Exception($"Error: '{file.FileName}' exceeds 5MB limit.");
        }

        var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), directory.ToString());
        if (!Directory.Exists(uploadPath))
        {
            Directory.CreateDirectory(uploadPath);
        }

        var fileName = $"{GenerateUlid()}.jpeg";
        var filePath = Path.Combine(uploadPath, fileName);

        try
        {
            using var fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);

            fileStream.Position = 0;

            return new FileUploadData
            {
                FilePath = filePath,
                FileName = fileName
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Error uploading '{file.FileName}': {ex.Message}");
        }
    }

    #endregion

    #region Get Base64 File Extension

    public static async Task<string> GetBase64FromFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new Exception($"File not found at path: {filePath}");
        }

        byte[] fileBytes = await File.ReadAllBytesAsync(filePath);
        return Convert.ToBase64String(fileBytes);
    }

    #endregion

    #region Hash Password

    public static string HashPassword(this string password, string username)
    {
        password = password.Trim();
        username = username.Trim();
        string saltedCode = EncodedSalt(username);
        string hashString;
        using (var sha256 = SHA256.Create())
        {
            var hash = sha256.ComputeHash(Encoding.Default.GetBytes(password + saltedCode));
            hashString = ToHex(hash, false);
        }

        return hashString;
    }

    private static string EncodedSalt(string decodeString)
    {
        decodeString = decodeString.ToLower()
            .Replace("a", "@")
            .Replace("i", "!")
            .Replace("l", "1")
            .Replace("e", "3")
            .Replace("o", "0")
            .Replace("s", "$")
            .Replace("n", "&");
        return decodeString;
    }

    public static string ToHex(byte[] bytes, bool upperCase)
    {
        StringBuilder result = new StringBuilder(bytes.Length * 2);
        for (int i = 0; i < bytes.Length; i++)
            result.Append(bytes[i].ToString(upperCase ? "X2" : "x2"));
        return result.ToString();
    }

    #endregion
}