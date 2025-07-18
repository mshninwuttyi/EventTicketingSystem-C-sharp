using System;
using System.Collections.Generic;

namespace EventTicketingSystem.CSharp.Database.AppDbContext;

public partial class TblVerification
{
    public string Verificationid { get; set; } = null!;

    public string Verificationcode { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime Expiredtime { get; set; }

    public bool Isused { get; set; }

    public string Createdby { get; set; } = null!;

    public DateTime Createdat { get; set; }

    public string? Modifiedby { get; set; }

    public DateTime? Modifiedat { get; set; }

    public bool Deleteflag { get; set; }
}
