using System.Security.Claims;

namespace EventTicketingSystem.CSharp.Domain.Services;

public class UserContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContextService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private ClaimsPrincipal? CurrentUser()
    {
        return _httpContextAccessor.HttpContext?.User;
    }

    public string? Username()
    {
        return CurrentUser()?.Identity?.Name ?? CurrentUser()?.FindFirst(ClaimTypes.Name)?.Value;
    }

    public string? SessionId()
    {
        return CurrentUser()?.FindFirst("sessionid")?.Value;
    }

    public string? AdminCode()
    {
        return CurrentUser()?.FindFirst("admincode")?.Value;
        
    }
}