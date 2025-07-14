using EventTicketingSystem.CSharp.Domain.Models.Features.Venue;

namespace EventTicketingSystem.CSharp.Domain.Features.Venue;

public class DA_Venue
{
    private readonly ILogger<DA_Venue> _logger;
    private readonly AppDbContext _db;

    public DA_Venue(ILogger<DA_Venue> logger, AppDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    public async Task<Result<List<VenueResponseModel>>> GetList()
    {
        var model = new VenueResponseModel();
        try
        {
            var data = await _db.TblVenues
                .AsNoTracking()
                .ToListAsync();

            var venues = data
                .Where(x => x.Deleteflag == false)
                .Select(x => new VenueResponseModel
                {
                    VenueId = x.Venueid,
                    VenueCode = x.Venuecode,
                    VenueTypeCode = x.Venuetypecode,
                    VenueName = x.Venuename,
                    VenueDescription = x.Venuedescription,
                    VenueAddress = x.Venueaddress,
                    VenueCapacity = x.Venuecapacity,
                    VenueFacilities = x.Venuefacilities,
                    VenueAddons = x.Venueaddons,
                    VenueImage = x.Venueimage
                }).ToList();

            return Result<List<VenueResponseModel>>.Success(venues, "Success!");
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            return Result<List<VenueResponseModel>>.SystemError(ex.Message);
        }
    }

    public async Task<Result<VenueResponseModel>> DeleteVenue(string venueid)
    {
        var model = new VenueResponseModel();
        try
        {
            var venue = await _db.TblVenues.FindAsync(venueid);
            
            if (venue == null)
                return Result<VenueResponseModel>.NotFoundError("Venue not found");
            
            venue.Deleteflag = true;
            await  _db.SaveChangesAsync();
            
            // Map updated entity to response model
            var venueResponse = new VenueResponseModel
            {
                VenueId = venue.Venueid,
                VenueCode = venue.Venuecode,
                VenueTypeCode = venue.Venuetypecode,
                VenueName = venue.Venuename,
                VenueDescription = venue.Venuedescription,
                VenueAddress = venue.Venueaddress,
                VenueCapacity = venue.Venuecapacity,
                VenueFacilities = venue.Venuefacilities,
                VenueAddons = venue.Venueaddons,
                VenueImage = venue.Venueimage,
                DeleteFlag = venue.Deleteflag
            };

            return Result<VenueResponseModel>.Success(venueResponse, "Success!");
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            return Result<VenueResponseModel>.SystemError(ex.Message);
        }
    }
}