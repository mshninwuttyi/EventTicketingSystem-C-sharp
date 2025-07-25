namespace EventTicketingSystem.CSharp.Domain.Models.Features.Venue;

public class EventEditResponseModel
{
    public EventEditModel? Event { get; set; }
}

public class EventEditModel
{
    public string? Eventid { get; set; }

    public string Eventcode { get; set; }

    public string Venuecode { get; set; }

    public string Eventname { get; set; }

    public string Eventcategorycode { get; set; }

    public DateTime Startdate { get; set; }

    public DateTime Enddate { get; set; }

    public bool Isactive { get; set; }

    public string Eventstatus { get; set; }

    public string Businessownercode { get; set; }

    public int Totalticketquantity { get; set; }

    public int Soldoutcount { get; set; }

    public string Uniquename { get; set; }

    public static EventEditModel FromTblEvent(TblEvent tblEvent)
    {
        return new EventEditModel
        {
            Eventid = tblEvent.Eventid,
            Eventcode = tblEvent.Eventcode,
            Venuecode = tblEvent.Venuecode,
            Eventname = tblEvent.Eventname,
            Eventcategorycode = tblEvent.Eventcategorycode,
            Startdate = tblEvent.Startdate,
            Enddate = tblEvent.Enddate,
            Isactive = tblEvent.Isactive,
            Eventstatus = tblEvent.Eventstatus,
            Businessownercode = tblEvent.Businessownercode,
            Totalticketquantity = tblEvent.Totalticketquantity,
            Soldoutcount = tblEvent.Soldoutcount,
            Uniquename = tblEvent.Uniquename,
        };
    }
}