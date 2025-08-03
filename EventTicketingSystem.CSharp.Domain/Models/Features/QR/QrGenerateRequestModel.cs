namespace EventTicketingSystem.CSharp.Domain.Models.Features.QR;

public class QrGenerateRequestModel
{
    public string TicketCode { get; set; }

    public string FullName { get; set; }

    public string Email { get; set; }

}