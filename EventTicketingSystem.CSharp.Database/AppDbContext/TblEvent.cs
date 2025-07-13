using System;
using System.Collections.Generic;

namespace EventTicketingSystem.CSharp.Database.AppDbContext;

public partial class TblEvent
{
    public string? Eventid { get; set; }

    public string? Eventcode { get; set; }

    public string? Eventname { get; set; }

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

    public int? Soldoutcount { get; set; }

    public string? Createdby { get; set; }

    public DateTime? Createdat { get; set; }

    public string? Modifiedby { get; set; }

    public DateTime? Modifiedat { get; set; }

    public bool? Deleteflag { get; set; }
}
