namespace EventTicketingSystem.CSharp.Domain.Models.Features.Auth;

public class RefreshResponseModel
{
    // The new access token (JWT)
    public string AccessToken { get; set; }

    // The new refresh token (rotated token)
    public string RefreshToken { get; set; }
}