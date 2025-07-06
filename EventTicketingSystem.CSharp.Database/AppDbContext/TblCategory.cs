using System;
using System.Collections.Generic;

namespace EventTicketingSystem.CSharp.Database.AppDbContext;

public partial class TblCategory
{
    public string Categoryid { get; set; } = null!;

    public string Categorycode { get; set; } = null!;

    public string Categoryname { get; set; } = null!;

    public string? Createby { get; set; }

    public DateTime? Createat { get; set; }

    public string? Modifiedby { get; set; }

    public DateTime? Modifiedat { get; set; }

    public bool? Deleteflag { get; set; }
}
