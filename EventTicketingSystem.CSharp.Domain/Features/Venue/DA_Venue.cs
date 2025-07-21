using EventTicketingSystem.CSharp.Domain.Models.Features.Venue;

namespace EventTicketingSystem.CSharp.Domain.Features.Venue;

public class DA_Venue
{
    private readonly ILogger<DA_Venue> _logger;
    private readonly AppDbContext _db;
    private readonly CommonService _commonService;

    public DA_Venue(ILogger<DA_Venue> logger, AppDbContext db, CommonService commonService)
    {
        _logger = logger;
        _db = db;
        _commonService = commonService;
    }
    
    public async Task<Result<List<VenueResponseModel>>> GetList()
    {
        var model = new VenueResponseModel();
        try
        {
            var data = await _db.TblVenues
                .AsNoTracking()
                .Where( x => x.Deleteflag == false)
                .OrderBy(x => x.Venuecode)
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
    public async Task<Result<VenueResponseModel>> CreateVenue(CreateVenueRequestModel createVenueRequest, string currentUserId)
    {
        // Map VenueRequestModel into the TblVenue Entities
        var venueEntity = new TblVenue()
        {
            Venueid = GenerateUlid(),
            Venuecode = await _commonService.GenerateSequenceCode(EnumTableUniqueName.Tbl_Venue),
            Venuetypecode = createVenueRequest.VenueTypeCode,
            Venuename = createVenueRequest.VenueName,
            Description = createVenueRequest.Description,
            Address = createVenueRequest.Address,
            Capacity = createVenueRequest.Capacity,
            Facilities = createVenueRequest.Facilities,
            Addons = createVenueRequest.Addons,
            Image = createVenueRequest.Image, // To Edit: Convert to the format to save into the db later
            Createdby = currentUserId,
            Createdat = DateTime.Now
        };
        
        // Add venueEntity to Db
        _db.TblVenues.Add(venueEntity);
        
        // Save changes
        await _db.SaveChangesAsync();
        
        // Map created entity to response model
        var venueResponse = MapToResponseModel(venueEntity);
        
        return Result<VenueResponseModel>.Success(venueResponse, "Venue created successfully.");
    }

    public async Task<Result<VenueResponseModel>> UpdateVenue(UpdateVenueRequestModel updateVenueRequest, string currentUserId)
    {
        // Find existing venue by ID
         var existingVenue = await _db.TblVenues.FindAsync(updateVenueRequest.VenueId);
        
        if(existingVenue == null)
            return Result<VenueResponseModel>.NotFoundError("Venue not found.");
        
        // Update
        existingVenue.Venuetypecode = updateVenueRequest.VenueTypeCode;
        existingVenue.Venuename = updateVenueRequest.VenueName;
        existingVenue.Description = updateVenueRequest.Description;
        existingVenue.Address = updateVenueRequest.Address;
        existingVenue.Capacity = updateVenueRequest.Capacity;
        existingVenue.Facilities = updateVenueRequest.Facilities;
        existingVenue.Addons = updateVenueRequest.Addons;
        existingVenue.Image = updateVenueRequest.Image;    // To Edit: save the actual location of the image later
        existingVenue.Deleteflag = updateVenueRequest.DeleteFlag;
        existingVenue.Modifiedby = currentUserId;
        existingVenue.Modifiedat = DateTime.Now;
        
        // Force EF to consider the entity modified
        _db.Entry(existingVenue).State = EntityState.Modified;
        
        // Save changes
        var rows = await _db.SaveChangesAsync();
        _logger.LogInformation(rows == 0 ? "no rows updated" : "there is an update");
        
        // Map updated entity to response model
        var venueResponse = MapToResponseModel(existingVenue);

        return Result<VenueResponseModel>.Success(venueResponse, "Venue updated successfully.");
    }
    
    public async Task<Result<VenueResponseModel>> DeleteVenue(string venueId, string currentUserId)
    {
        var model = new VenueResponseModel();
        try
        {
            var venue = await _db.TblVenues.FindAsync(venueId);
            
            if (venue == null)
                return Result<VenueResponseModel>.NotFoundError("Venue not found");
            
            _logger.LogInformation($"Found venue: {venue.Venuecode}, Deleteflag before update: {venue.Deleteflag}");

            venue.Deleteflag = true;
            venue.Modifiedby =  currentUserId;
            venue.Modifiedat =  DateTime.Now;
            
            // Force EF to consider the entity modified
            _db.Entry(venue).State = EntityState.Modified;
            
            await  _db.SaveChangesAsync();
            
            _logger.LogInformation($"Deleting venue ID: {venueId}, Deleteflag after save: {venue.Deleteflag}");
            
            // Map deleted entity to response model
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
            Description = venue.Description,
            Address = venue.Address,
            Capacity = venue.Capacity,
            Facilities = venue.Facilities,
            Addons = venue.Addons,
            Image = venue.Image,
            DeleteFlag = venue.Deleteflag,
            CreatedBy = venue.Createdby,
            CreatedAt = venue.Createdat,
            ModifiedBy = venue.Modifiedby,
            ModifiedAt = venue.Modifiedat
        };
    }
}