using System;
using System.Collections.Generic;

namespace EventTicketingSystem.CSharp.Database.AppDbContext;

public partial class TblBusinessowner
{
    public string? Businessownerid { get; set; }

    public string? Businessownercode { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Phonenumber { get; set; }

    public string? Createdby { get; set; }

    public DateTime? Createdat { get; set; }

    public string? Modifiedby { get; set; }

    public DateTime? Modifiedat { get; set; }

    public bool? Deleteflag { get; set; }
}
