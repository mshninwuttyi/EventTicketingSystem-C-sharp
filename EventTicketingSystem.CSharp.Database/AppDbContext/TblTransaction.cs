using System;
using System.Collections.Generic;

namespace EventTicketingSystem.CSharp.Database.AppDbContext;

public partial class TblTransaction
{
    public string? Transactionid { get; set; }

    public string? Transactioncode { get; set; }

    public string? Email { get; set; }

    public string? Eventcode { get; set; }

    public string? Status { get; set; }

    public string? Paymenttype { get; set; }

    public DateTime? Transactiondate { get; set; }

    public decimal? Totalamount { get; set; }

    public string? Createdby { get; set; }

    public DateTime? Createdat { get; set; }

    public string? Modifiedby { get; set; }

    public DateTime? Modifiedat { get; set; }

    public bool? Deleteflag { get; set; }
}
