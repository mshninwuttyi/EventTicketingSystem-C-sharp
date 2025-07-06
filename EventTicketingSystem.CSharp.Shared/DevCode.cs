namespace EventTicketingSystem.CSharp.Shared;

public static partial class DevCode
{
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

    #endregion

    #region Thousand Separators

    public static string ToThousandSeparator<T>(this T value) where T : struct, IFormattable
    {
        return value.ToString("N", null);
    }

    public static string ToDateTimeFormat(this DateTime date)
    {
        return date.ToString("dd-MM-yyyy");
    }

    #endregion

    #region Date Format MM Version

    public static string ToDateTimeFormat(this DateTime? date)
    {
        return date.HasValue ? date.Value.ToDateTimeFormat() : string.Empty;
    }

    public static string ToDateTimeFormatLong(this DateTime date)
    {
        return date.ToString("dd-MMMM-yyyy");
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
}