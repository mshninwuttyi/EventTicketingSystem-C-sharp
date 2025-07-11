using System;
using System.Collections.Generic;

namespace EventTicketingSystem.CSharp.Database.AppDbContext;

public partial class TblVenue
{
    public string? Venueid { get; set; }

    public string? Venuecode { get; set; }

    public string? Venuename { get; set; }

    public string? Venuedetailcode { get; set; }

    public string? Venuetypecode { get; set; }

    public string? Venuedescription { get; set; }

    public string? Venueaddress { get; set; }

    public int? Venuecapacity { get; set; }

    public string? Venuefacilities { get; set; }

    public string? Venueaddons { get; set; }

    public string? Createdby { get; set; }

    public DateTime? Createdat { get; set; }

    public string? Modifiedby { get; set; }

    public DateTime? Modifiedat { get; set; }

    public bool? Deleteflag { get; set; }
}
