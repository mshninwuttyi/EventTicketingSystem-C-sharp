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

    public string? Description { get; set; }

    public string? Address { get; set; }

    public int? Capacity { get; set; }

    public string? Facilities { get; set; }

    public string? Addons { get; set; }

    public List<string> VenueImage { get; set; }

    public string CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public static VenueListModel FromTblVenue(TblVenue venue)
    {
        var venueModel = new VenueListModel
        {
            VenueId = venue.Venueid,
            VenueCode = venue.Venuecode,
            VenueTypeCode = venue.Venuetypecode,
            VenueName = venue.Venuename,
            Description = venue.Description,
            Address = venue.Address,
            Capacity = venue.Capacity,
            Facilities = venue.Facilities,
            Addons = venue.Addons,
            VenueImage = new List<string>(),
            CreatedBy = venue.Createdby,
            CreatedAt = venue.Createdat,
            ModifiedBy = venue.Modifiedby,
            ModifiedAt = venue.Modifiedat,
        };

        if (!string.IsNullOrWhiteSpace(venue.Venueimage))
        {
            venueModel.VenueImage = venue.Venueimage
                .Split([','], StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToList();
        }

        return venueModel;
    }
}