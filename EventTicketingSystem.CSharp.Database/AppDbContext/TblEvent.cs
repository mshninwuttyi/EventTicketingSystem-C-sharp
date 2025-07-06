using System;
using System.Collections.Generic;

namespace EventTicketingSystem.CSharp.Database.AppDbContext;

public partial class TblEvent
{
    public string Eventid { get; set; } = null!;

    public string Eventcode { get; set; } = null!;

    public string Eventname { get; set; } = null!;

    public string? Categorycode { get; set; }

    public string? Description { get; set; }

    public string? Address { get; set; }

    public DateTime? Startdate { get; set; }

    public DateTime? Enddate { get; set; }

    public string? Eventimage { get; set; }

    public bool? Isactive { get; set; }

    public string? Eventstatus { get; set; }

    public string? Businessownercode { get; set; }

    public int? Totalticketquantity { get; set; }

    public string? Createby { get; set; }

    public DateTime? Createat { get; set; }

    public string? Modifiedby { get; set; }

    public DateTime? Modifiedat { get; set; }

    public bool? Deleteflag { get; set; }

    public string? Venueid { get; set; }

    public string? Venuecode { get; set; }

    public string? Venuename { get; set; }

    public string? Venuedescription { get; set; }

    public string? Venueaddress { get; set; }

    public int? Venuecapacity { get; set; }

    public string? Venueimage { get; set; }
}
