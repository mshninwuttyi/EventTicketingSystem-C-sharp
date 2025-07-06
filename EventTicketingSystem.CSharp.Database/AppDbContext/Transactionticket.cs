using System;
using System.Collections.Generic;

namespace EventTicketingSystem.CSharp.Database.AppDbContext;

public partial class Transactionticket
{
    public string Transactionticketid { get; set; } = null!;

    public string Transactioncode { get; set; } = null!;

    public string Ticketcode { get; set; } = null!;

    public string? Qistring { get; set; }

    public decimal Price { get; set; }

    public string? Createby { get; set; }

    public DateTime? Createat { get; set; }

    public string? Modifiedby { get; set; }

    public DateTime? Modifiedat { get; set; }

    public bool? Deleteflag { get; set; }
}
