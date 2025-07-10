using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTicketingSystem.CSharp.Domain.Models.Features.EventCategory
{
    public class EventCategoryRequestModel
    {
        //public string Categoryid { get; set; }

        public string? CategoryCode { get; set; }

        public string CategoryName { get; set; }

        public string AdminName { get; set; }
    }
}
