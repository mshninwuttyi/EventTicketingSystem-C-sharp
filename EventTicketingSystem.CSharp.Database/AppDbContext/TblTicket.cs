using System;
using System.Collections.Generic;

namespace EventTicketingSystem.CSharp.Database.AppDbContext;

public partial class TblTicket
{
    public string? Ticketid { get; set; }

    public string? Ticketcode { get; set; }

    public string? Ticketpricecode { get; set; }

    public bool? Isused { get; set; }

    public string? Createdby { get; set; }

    public DateTime? Createdat { get; set; }

    public string? Modifiedby { get; set; }

    public DateTime? Modifiedat { get; set; }

    public bool? Deleteflag { get; set; }
}
