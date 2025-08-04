using System;
using System.Collections.Generic;

namespace EventTicketingSystem.CSharp.Database.AppDbContext;

public partial class TblTicketprice
{
    public string Ticketpriceid { get; set; } = null!;

    public string Ticketpricecode { get; set; } = null!;

    public string Tickettypecode { get; set; } = null!;

    public decimal Ticketprice { get; set; }

    public int Ticketquantity { get; set; }

    public string Createdby { get; set; } = null!;

    public DateTime Createdat { get; set; }

    public string? Modifiedby { get; set; }

    public DateTime? Modifiedat { get; set; }

    public bool Deleteflag { get; set; }
}
