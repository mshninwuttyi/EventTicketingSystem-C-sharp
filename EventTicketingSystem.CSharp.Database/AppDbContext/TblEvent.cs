using System;
using System.Collections.Generic;

namespace EventTicketingSystem.CSharp.Database.AppDbContext;

public partial class TblEvent
{
    public string Eventid { get; set; } = null!;

    public string Eventcode { get; set; } = null!;

    public string Venuecode { get; set; } = null!;

    public string Eventname { get; set; } = null!;

    public string Eventcategorycode { get; set; } = null!;

    public DateTime Startdate { get; set; }

    public DateTime Enddate { get; set; }

    public bool Isactive { get; set; }

    public string Eventstatus { get; set; } = null!;

    public string Businessownercode { get; set; } = null!;

    public int Totalticketquantity { get; set; }

    public int Soldoutcount { get; set; }

    public string Uniquename { get; set; } = null!;

    public string Createdby { get; set; } = null!;

    public DateTime Createdat { get; set; }

    public string? Modifiedby { get; set; }

    public DateTime? Modifiedat { get; set; }

    public bool Deleteflag { get; set; }
}
