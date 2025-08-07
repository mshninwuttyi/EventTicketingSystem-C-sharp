namespace EventTicketingSystem.CSharp.Domain.Models.Features.Event;

public class EventCreateRequestModel
{
    public string Eventname { get; set; }
    
    
    public string Uniquename { get; set; }
    
    
    public string Eventcategorycode { get; set; }
    

    public string Businessownercode { get; set; }
    
    
    public string Venuecode { get; set; }

    public int Totalticketquantity { get; set; }
    
    public DateTime Startdate { get; set; }

    public DateTime Enddate { get; set; }
    

    public bool Isactive { get; set; }

}
