namespace EventTicketingSystem.CSharp.Domain.Models.Features.Auth;

public class RefreshTokenResponseModel
{
    // The new access token (JWT)
    public string Token  { get; set; }

    // The new refresh token (rotated token)
    public DateTime TokenExpiresAt  { get; set; } 
    
    public string RefreshToken { get; set; }
    
    public DateTime RefreshTokenExpiresAt { get; set; } 
}