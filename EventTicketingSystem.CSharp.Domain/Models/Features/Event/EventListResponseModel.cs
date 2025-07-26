namespace EventTicketingSystem.CSharp.Domain.Models.Features.Event;

public class EventListResponseModel
{
    public List<EventListModel>? EventList { get; set; }
}

public class EventListModel
{
    public string? Eventname { get; set; }

    public DateTime? Startdate { get; set; }

    public DateTime? Enddate { get; set; }

    public bool? Isactive { get; set; }

    public string? Eventstatus { get; set; }

    public int? Totalticketquantity { get; set; }

    public static EventListModel FromTblEvent(TblEvent tblEvent)
    {
        return new EventListModel
        {
            Eventname = tblEvent.Eventname,
            Startdate = tblEvent.Startdate,
            Enddate = tblEvent.Enddate,
            Eventstatus = tblEvent.Eventstatus,
        };
    }
}