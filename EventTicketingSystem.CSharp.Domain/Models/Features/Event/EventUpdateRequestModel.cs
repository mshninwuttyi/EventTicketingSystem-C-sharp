namespace EventTicketingSystem.CSharp.Domain.Models.Features.Event;

public class EventUpdateRequestModel
{
    public string EventCode { get; set; }

    public string EventName { get; set; }

    public string Description { get; set; }

    public string Address { get; set; }

    public DateTime Startdate { get; set; }

    public DateTime Enddate { get; set; }

    public string Eventimage { get; set; }

    public bool Isactive { get; set; }

    public string Eventstatus { get; set; }

    public int Totalticketquantity { get; set; }
}