using System.IdentityModel.Tokens.Jwt;

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
        var bearerToken = _contextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();
        if (string.IsNullOrEmpty(bearerToken) || !bearerToken.StartsWith("Bearer ")) return null;

        var token = bearerToken["Bearer ".Length..];

        var handler = new JwtSecurityTokenHandler();

        try
        {
            var jwtToken = handler.ReadJwtToken(token); 

            var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
            var sessionId = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;

            if (userId == null || sessionId == null)
                return null;

            return new ApiTokenModel
            {
                UserId = userId,
                SessionId = sessionId
            };
        }
        catch
        {
            return null; 
        }
    }

    protected string CurrentUserId => GetUser()?.UserId ?? string.Empty;
    protected string SessionId => GetUser()?.SessionId ?? string.Empty;

}

public class ApiTokenModel
{
    public string? UserId { get; set; } = "";
    public string? SessionId { get; set; } = "";
}
