namespace EventTicketingSystem.CSharp.Domain.Models.Features.BusinessEmail;

public class BusinessEmailListResponseModel
{
    public List<BusinessEmailModel> BusinessEmails { get; set; } = new List<BusinessEmailModel>();
}

public class BusinessEmailModel
{
    public string BusinessEmailId { get; set; }

    public string BusinessEmailCode { get; set; }

    public string FullName { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }
}