using System;
using System.Collections.Generic;

namespace EventTicketingSystem.CSharp.Database.AppDbContext;

public partial class TblVenuetype
{
    public string? Venuetypeid { get; set; }

    public string? Venuetypecode { get; set; }

    public string? Venuetypename { get; set; }

    public string? Createdby { get; set; }

    public DateTime? Createdat { get; set; }

    public string? Modifiedby { get; set; }

    public DateTime? Modifiedat { get; set; }

    public bool? Deleteflag { get; set; }
}
