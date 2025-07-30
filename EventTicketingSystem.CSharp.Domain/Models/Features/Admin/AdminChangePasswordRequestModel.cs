namespace EventTicketingSystem.CSharp.Domain.Features.Admin;

public class AdminChangePasswordRequestModel
{
    public string? AdminCode { get; set; }

    public string? UserName { get; set; }

    public string? OldPassword { get; set; }

    public string? NewPassword { get; set; }

    public string? ConfirmNewPassword { get; set; }
}