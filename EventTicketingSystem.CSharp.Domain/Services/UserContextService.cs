using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;

namespace EventTicketingSystem.CSharp.Domain.Services;

public class UserContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContextService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    private ClaimsPrincipal? CurrentUser() => _httpContextAccessor.HttpContext?.User;

    public string? Username => CurrentUser()?.FindFirst(JwtRegisteredClaimNames.UniqueName)?.Value;

    public string? SessionId => CurrentUser()?.FindFirst("sessionid")?.Value;

    public string? AdminCode => CurrentUser()?.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

}