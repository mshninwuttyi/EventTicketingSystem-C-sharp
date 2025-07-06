using System;
using System.Collections.Generic;

namespace EventTicketingSystem.CSharp.Database.AppDbContext;

public partial class TblTransaction
{
    public string Transactionid { get; set; } = null!;

    public string Transactioncode { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Eventcode { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string? Paymenttype { get; set; }

    public DateTime Transactiondate { get; set; }

    public decimal Totalamount { get; set; }

    public string? Createby { get; set; }

    public DateTime? Createat { get; set; }

    public string? Modifiedby { get; set; }

    public DateTime? Modifiedat { get; set; }

    public bool? Deleteflag { get; set; }
}
