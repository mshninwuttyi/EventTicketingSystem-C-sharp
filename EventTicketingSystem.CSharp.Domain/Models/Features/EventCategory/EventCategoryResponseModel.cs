using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTicketingSystem.CSharp.Domain.Features.EventCategory
{
    public class EventCategoryResponseModel
    {
        public List<EventCategoryModel> EventCategories { get; set; }
    }
    
    public class EventCategoryModel
    {
        public string Categoryid { get; set; }

        public string Categorycode { get; set; }

        public string Categoryname { get; set; }

        //public string? Createdby { get; set; }

        //public DateTime? Createdat { get; set; }

        //public string? Modifiedby { get; set; }

        //public DateTime? Modifiedat { get; set; }

        //public bool? Deleteflag { get; set; }
    }

}
