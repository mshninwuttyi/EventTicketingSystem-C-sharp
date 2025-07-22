using System;
using System.Collections.Generic;

namespace EventTicketingSystem.CSharp.Database.AppDbContext;

public partial class TblSequence
{
    public int Sequenceid { get; set; }

    public string Uniquename { get; set; } = null!;

    public string Sequenceno { get; set; } = null!;

    public DateTime Sequencedate { get; set; }

    public string Sequencetype { get; set; } = null!;

    public string? Eventcode { get; set; }

    public bool Deleteflag { get; set; }
}
