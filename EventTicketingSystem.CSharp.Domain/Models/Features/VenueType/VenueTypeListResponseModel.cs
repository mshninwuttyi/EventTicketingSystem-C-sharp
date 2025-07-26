using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTicketingSystem.CSharp.Domain.Models.Features.VenueType
{
    public class VenueTypeListResponseModel
    {
        public List<VenueTypeListModel> VenueTypeList { get; set; }
    }
    public class VenueTypeListModel
    {
        public string VenueTypeid { get; set; }
        public string VenueTypeCode { get; set; }
        public string VenueTypename { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }

        public static VenueTypeListModel FromTblVenueType(TblVenuetype tblVenuetype)
        {
            return new VenueTypeListModel
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
