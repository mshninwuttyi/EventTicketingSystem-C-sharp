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

            return Result<List<VenueResponseModel>>.Success(venues, "Venue list retrieved successfully.");
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
            Description = venue.VenueDescription,
            Address = venue.VenueAddress,
            Capacity = venue.VenueCapacity,
            Facilities = venue.VenueFacilities,
            Addons = venue.VenueAddons,
            Image = venue.VenueImage, // To Edit: Convert to the format to save into the db later
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
        
        return Result<VenueResponseModel>.Success(venueResponse, "Venue created successfully.");
    }

    public async Task<Result<VenueResponseModel>> UpdateVenue(VenueRequestModel venue, string currentUserId)
    {
        // Find existing venue by ID
        var existingVenue = await _db.TblVenues.FindAsync(venue.VenueId);
        
        if(existingVenue == null)
            return Result<VenueResponseModel>.NotFoundError("Venue not found.");
        
        // Update
        existingVenue.Venuetypecode = venue.VenueTypeCode;
        existingVenue.Venuename = venue.VenueName;
        existingVenue.Description = venue.VenueDescription;
        existingVenue.Address = venue.VenueAddress;
        existingVenue.Capacity = venue.VenueCapacity;
        existingVenue.Facilities = venue.VenueFacilities;
        existingVenue.Addons = venue.VenueAddons;
        existingVenue.Image = venue.VenueImage;    // To Edit: save the actual location of the image later
        existingVenue.Deleteflag = venue.DeleteFlag;
        existingVenue.Modifiedby = currentUserId;
        existingVenue.Modifiedat = DateTime.Now;
        
        // Save changes
        await _db.SaveChangesAsync();
        
        // Map created entity to response model
        var venueResponse = MapToResponseModel(existingVenue);

        return Result<VenueResponseModel>.Success(venueResponse, "Venue updated successfully.");
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
            
            return Result<VenueResponseModel>.Success(venueResponse, "Venue deleted successfully.");
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
            VenueDescription = venue.Description,
            VenueAddress = venue.Address,
            VenueCapacity = venue.Capacity,
            VenueFacilities = venue.Facilities,
            VenueAddons = venue.Addons,
            VenueImage = venue.Image,
            DeleteFlag = venue.Deleteflag,
            CreatedBy = venue.Createdby,
            CreatedAt = venue.Createdat,
            ModifiedBy = venue.Modifiedby,
            ModifiedAt = venue.Modifiedat
        };
    }
    
    private string GenerateVenueCode()
    {
        var count = _db.TblVenues.Count();
        return $"VE{(count + 1):D6}";
    }

}