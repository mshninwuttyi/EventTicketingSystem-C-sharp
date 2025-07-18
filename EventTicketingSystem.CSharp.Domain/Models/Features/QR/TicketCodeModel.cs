namespace EventTicketingSystem.CSharp.Domain.Models.Features.QR;

public class TicketCodeModel
{
    public string TicketId { get; set; }

    public string TicketCode { get; set; }

    public string TicketPriceCode { get; set; }

    public bool IsUsed { get; set; }
}