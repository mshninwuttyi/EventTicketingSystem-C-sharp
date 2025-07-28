using System.Security.Claims;

namespace EventTicketingSystem.CSharp.Shared.Extensions;

public static class UserClaimsExtensions
{
    public static string GetCurrentUserId(this ClaimsPrincipal user)
    {
        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new UnauthorizedAccessException("Missing user ID in JWT claims.");
        }
        
        return userId;
    }
}