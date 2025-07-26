using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTicketingSystem.CSharp.Domain.Models.Features.TicketType;

public class TicketTypeCreateRequestModel
{
    public string EventCode { get; set; }

    public string TicketTypeName { get; set; }

    public decimal Ticketprice { get; set; }

    public int TicketQuantity { get; set; }
}
