using System;
using System.Collections.Generic;

namespace EventTicketingSystem.CSharp.Database.AppDbContext;

public partial class TblTransactionticket
{
    public string Transactionticketid { get; set; } = null!;

    public string Transactionticketcode { get; set; } = null!;

    public string Transactioncode { get; set; } = null!;

    public string Ticketcode { get; set; } = null!;

    public string Qrimage { get; set; } = null!;

    public decimal Price { get; set; }

    public string Createdby { get; set; } = null!;

    public DateTime Createdat { get; set; }

    public string? Modifiedby { get; set; }

    public DateTime? Modifiedat { get; set; }

    public bool Deleteflag { get; set; }
}
