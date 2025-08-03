namespace EventTicketingSystem.CSharp.Shared.Extensions;

public abstract class AuthorizationService
{
    private readonly IHttpContextAccessor _contextAccessor;

    protected AuthorizationService(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    private ApiTokenModel? GetUser()
    {
        var encryptedToken = _contextAccessor.HttpContext!.Request.Headers["Authorization"].ToString();
        if (encryptedToken.IsNullOrEmpty()) return null;
        var str = encryptedToken.Substring("Bearer ".Length);
        var token = str.ToDecrypt();
        string[] value = token.Split(".");
        var model = new ApiTokenModel
        {
            UserId = value[0],
            SessionId = value[1],
        };

        return model;
    }

    protected string CurrentUserId => GetUser()?.UserId!;
    protected string SessionId => GetUser()?.SessionId!;
}

public class ApiTokenModel
{
    public string? UserId { get; set; } = "";
    public string? SessionId { get; set; } = "";
}
