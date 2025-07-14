using System;
using System.Collections.Generic;

namespace EventTicketingSystem.CSharp.Database.AppDbContext;

public partial class TblTransactionticket
{
    public string? Transactionticketid { get; set; }

    public string? Transactioncode { get; set; }

    public string? Ticketcode { get; set; }

    public string? Qrstring { get; set; }

    public decimal? Price { get; set; }

    public string? Createdby { get; set; }

    public DateTime? Createdat { get; set; }

    public string? Modifiedby { get; set; }

    public DateTime? Modifiedat { get; set; }

    public bool? Deleteflag { get; set; }
}
