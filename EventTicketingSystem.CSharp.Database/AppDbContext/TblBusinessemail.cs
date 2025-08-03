using System;
using System.Collections.Generic;

namespace EventTicketingSystem.CSharp.Database.AppDbContext;

public partial class TblBusinessemail
{
    public string Businessemailid { get; set; } = null!;

    public string Businessemailcode { get; set; } = null!;

    public string Fullname { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Createdby { get; set; } = null!;

    public DateTime Createdat { get; set; }

    public string? Modifiedby { get; set; }

    public DateTime? Modifiedat { get; set; }

    public bool Deleteflag { get; set; }
}
