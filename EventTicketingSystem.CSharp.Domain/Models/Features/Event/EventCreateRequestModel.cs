namespace EventTicketingSystem.CSharp.Domain.Models.Features.Event;

public class EventCreateRequestModel
{
    public string Eventname { get; set; }

    public string Description { get; set; }

    public string Address { get; set; }

    public DateTime Startdate { get; set; }

    public DateTime Enddate { get; set; }

    public bool Isactive { get; set; }

    public string Eventstatus { get; set; }

    public int Totalticketquantity { get; set; }
}
