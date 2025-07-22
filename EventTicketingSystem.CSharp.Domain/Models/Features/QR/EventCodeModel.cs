namespace EventTicketingSystem.CSharp.Domain.Models.Features.QR;

public class EventCodeModel
{
    public string EventId { get; set; }

    public string EventCode { get; set; }

    public string EventName { get; set; }

    public string CategoryCode { get; set; }

    public string Description { get; set; }

    public string Address { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string EventImage { get; set; }

    public bool IsActive { get; set; }

    public string EventStatus { get; set; }

    public string BusinessOwnerCode { get; set; }

    public int TotalTicketQuantity { get; set; }

    public int SoldoutCount { get; set; }
}