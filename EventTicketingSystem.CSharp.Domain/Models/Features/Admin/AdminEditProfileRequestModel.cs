namespace EventTicketingSystem.CSharp.Domain.Models.Features.Admin;

public class AdminEditProfileRequestModel
{
    public string AdminCode { get; set; }

    public string FullName { get; set; }

    public string Phone { get; set; }

    public IFormFile ProfileImage { get; set; }
}