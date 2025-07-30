using System.Security.Claims;

namespace EventTicketingSystem.CSharp.Shared.Extensions;

public static class UserClaimsExtensions
{
    public static string? GetCurrentSessionId(this ClaimsPrincipal user)
    {
        return user.FindFirst("sessionid")?.Value;
    }
    
    public static string GetCurrentAdminCode(this ClaimsPrincipal user)
    {
        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return userId;
    }
    
    public static string? GetCurrentUsername(this ClaimsPrincipal user)
    {
        return user.Identity?.Name ?? user.FindFirst(ClaimTypes.Name)?.Value;
    }
    
    
}