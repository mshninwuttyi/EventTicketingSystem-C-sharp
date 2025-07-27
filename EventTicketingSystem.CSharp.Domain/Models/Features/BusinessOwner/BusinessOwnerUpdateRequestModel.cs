namespace EventTicketingSystem.CSharp.Domain.Models.Features.BusinessOwner;

public class BusinessOwnerUpdateRequestModel
{
    public string Businessownerid { get; set; }

    public string Businessownercode { get; set; }

    public string FullName { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }
}