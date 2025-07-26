using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTicketingSystem.CSharp.Domain.Models.Features.TicketType
{
    public class TicketTypeUpdateRequestModel
    {
        public string Tickettypecode { get; set; }
        public string Tickettypename { get; set; }
    }
}
