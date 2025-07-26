using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTicketingSystem.CSharp.Domain.Models.Features.VenueType
{
    public class VenueTypeUpdateRequestModel
    {
        public string VenueTypeCode { get; set; }
        public string VenueTypeName { get; set; }
    }
}
