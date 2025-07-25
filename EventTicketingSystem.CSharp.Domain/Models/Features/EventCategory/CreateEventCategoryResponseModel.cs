using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTicketingSystem.CSharp.Domain.Models.Features.EventCategory
{
    public class CreateEventCategoryResponseModel
    {
        public string? EventCategoryid { get; set; }

        public string? EventCategorycode { get; set; }

        public string? Categoryname { get; set; }

        public string? Createdby { get; set; }

        public DateTime? Createdat { get; set; }
    }

}
