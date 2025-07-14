namespace EventTicketingSystem.CSharp.Domain.Models.Features.BusinessOwner;

public class BusinessOwnerResponseModel
{
    public List<BusinessOwnerModel> BusinessOwners { get; set; }
}

public class BusinessOwnerModel
{
    public string BusinessOwnerId { get; set; }
    public string BusinessOwnerCode { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}