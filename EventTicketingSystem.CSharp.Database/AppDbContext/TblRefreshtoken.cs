using System;
using System.Collections.Generic;

namespace EventTicketingSystem.CSharp.Database.AppDbContext;

public partial class TblRefreshtoken
{
    public string Refreshtokenid { get; set; } = null!;

    public string Admincode { get; set; } = null!;

    public string Token { get; set; } = null!;

    public DateTime Expirydate { get; set; }

    public bool Isrevoked { get; set; }

    public DateTime Createdat { get; set; }

    public DateTime? Revokedat { get; set; }
}
