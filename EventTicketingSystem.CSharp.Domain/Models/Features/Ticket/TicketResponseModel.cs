using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTicketingSystem.CSharp.Domain.Models.Features.Ticket
{
    public class TicketResponseModel
    {
        public string? Ticketid { get; set; }

        public string? Ticketcode { get; set; }

        public string? Ticketpricecode { get; set; }

        public bool? Isused { get; set; }

    }
    public class TicketListResponseModel
    {
        public List<TicketResponseModel> Tickets { get; set; } = new List<TicketResponseModel>();

    }
}
