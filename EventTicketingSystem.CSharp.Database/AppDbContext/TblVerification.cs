using System;
using System.Collections.Generic;

namespace EventTicketingSystem.CSharp.Database.AppDbContext;

public partial class TblVerification
{
    public string Verificationid { get; set; } = null!;

    public string Verificationcode { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Createdby { get; set; }

    public DateTime? Createdat { get; set; }

    public string? Modifiedby { get; set; }

    public DateTime? Modifiedat { get; set; }

    public bool? Deleteflag { get; set; }

    public string? Businessemailid { get; set; }

    public string? Businessemailcode { get; set; }

    public string? Fullname { get; set; }

    public string? Phone { get; set; }

    public string? Createby { get; set; }

    public DateTime? Createat { get; set; }
}
