using System;
using System.Collections.Generic;

namespace EventTicketingSystem.CSharp.Database.AppDbContext;

public partial class TblLogin
{
    public string Loginid { get; set; } = null!;

    public string Admincode { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Sessionid { get; set; } = null!;

    public string Refreshtoken { get; set; } = null!;

    public DateTime Refreshtokenexpiresat { get; set; }

    public string Loginstatus { get; set; } = null!;

    public string Createdby { get; set; } = null!;

    public DateTime Createdat { get; set; }

    public string? Modifiedby { get; set; }

    public DateTime? Modifiedat { get; set; }

    public bool Deleteflag { get; set; }
}
