namespace EventTicketingSystem.CSharp.Domain.Models.Features.Venue;

public class VenueListResponseModel
{
    public List<VenueListModel>? VenueList { get; set; }
}

public class VenueListModel
{
    public string VenueId { get; set; }
    public string VenueCode { get; set; }
    public string VenueTypeCode { get; set; }
    public string VenueName { get; set; }
    public int? Capacity { get; set; }
    public static VenueListModel FromTblVenue(TblVenue venue)
    {
        return new VenueListModel
        {
            VenueId = venue.Venueid,
            VenueCode = venue.Venuecode,
            VenueTypeCode = venue.Venuetypecode,
            VenueName = venue.Venuename,
            Capacity = venue.Capacity
        };
    }
}