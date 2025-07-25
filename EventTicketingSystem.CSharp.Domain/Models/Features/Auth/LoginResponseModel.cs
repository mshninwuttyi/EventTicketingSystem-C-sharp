namespace EventTicketingSystem.CSharp.Domain.Models.Features.Auth;

public class LoginResponseModel
{
    public string Token { get; set; }
    
    public DateTime TokenExpiresAt  { get; set; }
}