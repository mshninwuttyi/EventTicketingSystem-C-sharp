using System;
using System.Collections.Generic;

namespace EventTicketingSystem.CSharp.Database.AppDbContext;

public partial class TblAdmin
{
    public string Adminid { get; set; } = null!;

    public string Admincode { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phoneno { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Profileimage { get; set; } = null!;

    public string Createdby { get; set; } = null!;

    public DateTime Createdat { get; set; }

    public string? Modifiedby { get; set; }

    public DateTime? Modifiedat { get; set; }

    public bool Deleteflag { get; set; }
}
