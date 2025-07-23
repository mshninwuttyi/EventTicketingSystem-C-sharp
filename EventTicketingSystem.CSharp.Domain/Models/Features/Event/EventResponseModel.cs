using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTicketingSystem.CSharp.Domain.Models.Features.Event
{
    public class EventListResponseModel
    {
        public List<EventResponseModel> EventListResponse { get; set; }
    }
    public class EventResponseModel
    {

        public string? Eventname { get; set; }

        public string? Description { get; set; }

        public string? Address { get; set; }

        public DateTime? Startdate { get; set; }

        public DateTime? Enddate { get; set; }

        public string? Eventimage { get; set; }

        public bool? Isactive { get; set; }

        public string? Eventstatus { get; set; }
        public int? Totalticketquantity { get; set; }


    }
}
