using System;
using System.Collections.Generic;

namespace EventTicketingSystem.CSharp.Database.AppDbContext;

public partial class TblTickettype
{
    public string Tickettypeid { get; set; } = null!;

    public string Tickettypecode { get; set; } = null!;

    public string Tickettypename { get; set; } = null!;

    public string? Createby { get; set; }

    public DateTime? Createat { get; set; }

    public string? Modifiedby { get; set; }

    public DateTime? Modifiedat { get; set; }

    public bool? Deleteflag { get; set; }
}
