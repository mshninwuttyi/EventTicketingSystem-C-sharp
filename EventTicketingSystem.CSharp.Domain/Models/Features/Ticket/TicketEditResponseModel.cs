using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTicketingSystem.CSharp.Domain.Models.Features.Ticket
{
    public class TicketEditResponseModel
    {
        public string? Ticketid { get; set; }

        public string? Ticketcode { get; set; }

        public string? Ticketpricecode { get; set; }

        public bool? Isused { get; set; }

        public string? Createdby { get; set; }

        public DateTime? Createdat { get; set; }

        public string? Modifiedby { get; set; }

        public DateTime? Modifiedat { get; set; }
    }
}
