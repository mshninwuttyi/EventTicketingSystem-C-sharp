﻿using System;
using System.Collections.Generic;

namespace EventTicketingSystem.CSharp.Database.AppDbContext;

public partial class TblBusinessowner
{
    public string Businessownerid { get; set; } = null!;

    public string Businessownercode { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phonenumber { get; set; } = null!;

    public string Createdby { get; set; } = null!;

    public DateTime Createdat { get; set; }

    public string? Modifiedby { get; set; }

    public DateTime? Modifiedat { get; set; }

    public bool Deleteflag { get; set; }
}
