namespace EventTicketingSystem.CSharp.Domain.Models.Features.QR;

public class QrGenerateRequestModel
{
    public string EventCode { get; set; }
    public string TicketCode { get; set; }
    public string TicketPriceCode { get; set; }
    public string TicketTypeCode { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
}
