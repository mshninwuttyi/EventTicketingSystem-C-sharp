namespace EventTicketingSystem.CSharp.Domain.Models.Features.VenueType;

public class VenueTypeEditResponseModel
{
    public VenueTypeEditModel? VenueTypeEdit { get; set; }
}

public class VenueTypeEditModel
{
    public string VenueTypeCode { get; set; }
    public string VenueTypename { get; set; }

    public static VenueTypeEditModel FromTblVenueType (TblVenuetype tblVenuetype)
    {
        return new VenueTypeEditModel
        {
            VenueTypeCode = tblVenuetype.Venuetypecode,
            VenueTypename = tblVenuetype.Venuetypename,
        };
    }
}