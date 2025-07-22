namespace EventTicketingSystem.CSharp.Domain.Models.Features.BusinessOwner;

public class BusinessOwnerRequestModel
{
    public string Businessownerid { get; set; }

    public string Businessownercode { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Phonenumber { get; set; }

    public string Admin { get; set; }

    public bool Deleteflag { get; set; }
}