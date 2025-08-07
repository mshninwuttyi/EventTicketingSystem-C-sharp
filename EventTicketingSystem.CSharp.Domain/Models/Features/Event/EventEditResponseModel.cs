namespace EventTicketingSystem.CSharp.Domain.Models.Features.Event;

public class EventEditResponseModel
{
    public EventEditModel? Event { get; set; }
}

public class EventEditModel
{
    public string Eventcode { get; set; }
    
    public string Eventcategory { get; set; }

    public string Eventname { get; set; }

    public string Uniquename { get; set; }

    public string? Businessownername { get; set; }
    
    public string? Venuename { get; set; }

    public string? Venuetypename { get; set; }
    
    public int? Capacity { get; set; }
    
    public string? Description { get; set; }
    
    public string? Facilities { get; set; }
    
    public List<string>? Addons { get; set; }
    
    public List<string>? VenueImage { get; set; }
    
    public string? Address { get; set; }

    public DateTime Startdate { get; set; }

    public DateTime Enddate { get; set; }

    public int Totalticketquantity { get; set; }
    
    public int Soldoutcount { get; set; }

    public string Eventstatus { get; set; }

    public bool Isactive { get; set; }

    public static EventEditModel FromTblEvent(TblEvent tblEvent)
    {
        var eventModel = new EventEditModel
        {
            Eventcode = tblEvent.Eventcode,
            Eventname = tblEvent.Eventname,
            Startdate = tblEvent.Startdate,
            Enddate = tblEvent.Enddate,
            Isactive = tblEvent.Isactive,
            Eventstatus = tblEvent.Eventstatus,
            Totalticketquantity = tblEvent.Totalticketquantity,
            Soldoutcount = tblEvent.Soldoutcount,
            Uniquename = tblEvent.Uniquename,
        };

        return eventModel;
    }
}