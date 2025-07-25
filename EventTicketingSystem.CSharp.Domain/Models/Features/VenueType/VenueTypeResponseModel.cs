using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTicketingSystem.CSharp.Domain.Models.Features.VenueType
{
    public class VenueTypeResponseModel
    {
        public List<VenueTypeModel> VenueTypes { get; set; }
    }

    public class VenueTypeModel
    {
        public string Venuetypeid { get; set; }

        public string Venuetypecode { get; set; }

        public string Venuetypename { get; set; }

        public string Createdby { get; set; }

        public DateTime Createdat { get; set; }

        public string? Modifiedby { get; set; }

        public DateTime? Modifiedat { get; set; }

        public bool Deleteflag { get; set; }
    }

}
