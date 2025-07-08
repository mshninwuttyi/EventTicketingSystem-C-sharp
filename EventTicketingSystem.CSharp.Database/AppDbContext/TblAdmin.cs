using System;
using System.Collections.Generic;

namespace EventTicketingSystem.CSharp.Database.AppDbContext;

public partial class TblAdmin
{
    public string? Userid { get; set; }

    public string? Usercode { get; set; }

    public string? Username { get; set; }

    public string? Email { get; set; }

    public string? Phoneno { get; set; }

    public string? Password { get; set; }

    public string? Createdby { get; set; }

    public DateTime? Createdat { get; set; }

    public string? Modifiedby { get; set; }

    public DateTime? Modifiedat { get; set; }

    public bool? Deleteflag { get; set; }
}
