using System;
using System.Collections.Generic;

namespace EventTicketingSystem.CSharp.Database.AppDbContext;

public partial class TblVenue
{
    public string Venueid { get; set; } = null!;

    public string Venuecode { get; set; } = null!;

    public string Venuetypecode { get; set; } = null!;

    public string Venuename { get; set; } = null!;

    public string? Description { get; set; }

    public string Address { get; set; } = null!;

    public int Capacity { get; set; }

    public string? Facilities { get; set; }

    public string? Addons { get; set; }

    public string Image { get; set; } = null!;

    public string Createdby { get; set; } = null!;

    public DateTime Createdat { get; set; }

    public string? Modifiedby { get; set; }

    public DateTime? Modifiedat { get; set; }

    public bool Deleteflag { get; set; }
}
