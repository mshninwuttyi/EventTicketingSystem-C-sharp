using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTicketingSystem.CSharp.Domain.Models.Features.VenueType
{
    public class VenueTypeEditResponseModel
    {
        public VenueTypeEditModel? VenueTypeEdit { get; set; }
    }

    public class VenueTypeEditModel
    {
        public string VenueTypeid { get; set; }
        public string VenueTypeCode { get; set; }
        public string VenueTypename { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } 

        public static VenueTypeEditModel FromTblVenueType (TblVenuetype tblVenuetype)
        {
            return new VenueTypeEditModel
            {
                VenueTypeid = tblVenuetype.Venuetypeid,
                VenueTypeCode = tblVenuetype.Venuetypecode,
                VenueTypename = tblVenuetype.Venuetypename,
                CreatedBy = tblVenuetype.Createdby,
                CreatedAt = tblVenuetype.Createdat
            };
        }
    }
}
