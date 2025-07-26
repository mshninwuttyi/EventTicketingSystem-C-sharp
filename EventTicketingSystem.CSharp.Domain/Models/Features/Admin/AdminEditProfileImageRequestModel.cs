namespace EventTicketingSystem.CSharp.Domain.Models.Features.Admin;

public class AdminEditProfileImageRequestModel
{
    public string AdminCode { get; set; }
    public IFormFile ProfileImage { get; set; }
}