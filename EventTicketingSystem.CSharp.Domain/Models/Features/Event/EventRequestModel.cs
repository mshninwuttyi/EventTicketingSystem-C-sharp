using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTicketingSystem.CSharp.Domain.Models.Features.Event
{
 
    public class EventRequestModel
    {
        public string? Eventcode{ get; set; } 
        public string? Admin {  get; set; }
        public string Eventname { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public DateTime Startdate { get; set; }

        public DateTime Enddate { get; set; }

        public string Eventimage { get; set; }

        public bool Isactive { get; set; }

        public string Eventstatus { get; set; }

        public int Totalticketquantity { get; set; }

    }
}
