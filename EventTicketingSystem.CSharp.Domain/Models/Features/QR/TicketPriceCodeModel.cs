namespace EventTicketingSystem.CSharp.Domain.Models.Features.QR;

public class TicketPriceCodeModel
{
    public string TicketPriceId { get; set; }

    public string TicketPriceCode { get; set; }

    public string EventCode { get; set; }

    public string TicketTypeCode { get; set; }

    public decimal TicketPriceAmount { get; set; }

    public int TicketQuantity { get; set; }
}