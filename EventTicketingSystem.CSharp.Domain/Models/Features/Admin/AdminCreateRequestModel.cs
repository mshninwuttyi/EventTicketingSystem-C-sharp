namespace EventTicketingSystem.CSharp.Domain.Models.Features.Admin;

public class AdminCreateRequestModel
{
    public string Username { get; set; }

    public string Email { get; set; }

    public string PhoneNo { get; set; }

    public string Password { get; set; }
}