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
                .Where( x => x.Deleteflag == false)
                .ToListAsync();
            
            var venues = data
                .Select(MapToResponseModel)
                .ToList();

            return Result<List<VenueResponseModel>>.Success(venues, "Success!");
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            return Result<List<VenueResponseModel>>.SystemError(ex.Message);
        }
    }
    public async Task<Result<VenueResponseModel>> CreateVenue(VenueRequestModel venue, string currentUserId)
    {
        // Map VenueRequestModel into the TblVenue Entities
        var venueEntity = new TblVenue()
        {
            Venuename = venue.VenueName,
            Venuedescription = venue.VenueDescription,
            Venueaddress = venue.VenueAddress,
            Venuecapacity = venue.VenueCapacity,
            Venuefacilities = venue.VenueFacilities,
            Venueaddons = venue.VenueAddons,
            Venueimage = venue.VenueImage, // To Edit: Convert to the format to save into the db later
            Createdby = currentUserId,
            Createdat = DateTime.Now,
            
            // Generate Venue Code
            Venueid = $"VE{Ulid.NewUlid().ToString().Substring(0, 8).ToUpper()}",
            Venuecode = GenerateVenueCode(), // To Edit: Get VenueCode using the sequence table later
            Venuetypecode = "VT000001"    // To Edit: Get VenueTypeCode from VenueType table later
        };
        
        // Add venueEntity to Db
        _db.TblVenues.Add(venueEntity);
        
        // Save changes
        await _db.SaveChangesAsync();
        
        // Map created entity to response model
        var venueResponse = MapToResponseModel(venueEntity);
        
        return Result<VenueResponseModel>.Success(venueResponse, "Success!");
    }
    
    public async Task<Result<VenueResponseModel>> DeleteVenue(string venueid, string currentUserId)
    {
        var model = new VenueResponseModel();
        try
        {
            var venue = await _db.TblVenues.FindAsync(venueid);
            
            if (venue == null)
                return Result<VenueResponseModel>.NotFoundError("Venue not found");
            
            venue.Deleteflag = true;
            venue.Modifiedby =  currentUserId;
            venue.Modifiedat =  DateTime.Now;
            
            await  _db.SaveChangesAsync();
            
            // Map updated entity to response model
            var venueResponse = MapToResponseModel(venue);
            
            return Result<VenueResponseModel>.Success(venueResponse, "Success!");
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            return Result<VenueResponseModel>.SystemError(ex.Message);
        }
    }
    private static VenueResponseModel MapToResponseModel(TblVenue venue)
    {
        return new VenueResponseModel
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
            DeleteFlag = venue.Deleteflag,
            CreatedBy = venue.Createdby,
            CreatedAt = venue.Createdat,
            ModifiedBy = venue.Modifiedby,
            ModifiedAt = venue.Modifiedat
        };
    }
    
    private string GenerateVenueCode()
    {
        var count = _db.TblCategories.Count();
        return $"VE{(count + 1):D6}";
    }

}