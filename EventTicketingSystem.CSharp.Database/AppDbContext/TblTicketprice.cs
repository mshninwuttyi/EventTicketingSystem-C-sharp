using System;
using System.Collections.Generic;

namespace EventTicketingSystem.CSharp.Database.AppDbContext;

public partial class TblTicketprice
{
    public string? Ticketpriceid { get; set; }

    public string? Ticketpricecode { get; set; }

    public string? Eventcode { get; set; }

    public string? Tickettypecode { get; set; }

    public decimal? Ticketprice { get; set; }

    public int? Ticketquantity { get; set; }

    public string? Createdby { get; set; }

    public DateTime? Createdat { get; set; }

    public string? Modifiedby { get; set; }

    public DateTime? Modifiedat { get; set; }

    public bool? Deleteflag { get; set; }
    public virtual TblTickettype? TicketType { get; set; }
    public virtual ICollection<TblTicket> Tickets { get; set; } = new List<TblTicket>();
}
