namespace EventTicketingSystem.CSharp.Domain.Features.Venue;

public class DA_Venue
{
    private readonly ILogger<DA_Venue> _logger;
    private readonly AppDbContext _db;
    private readonly CommonService _commonService;
    private const string CreatedByUserId = "Admin";
    
    public DA_Venue(ILogger<DA_Venue> logger, AppDbContext db, CommonService commonService)
    {
        _logger = logger;
        _db = db;
        _commonService = commonService;
    }

    #region Get Venue List
    public async Task<Result<VenueListResponseModel>> List()
    {
        var model = new VenueListResponseModel();
        try
        {
            var data = await _db.TblVenues
                        .Where(x => x.Deleteflag == false)
                        .OrderBy(x => x.Venueid)
                        .ToListAsync();
            if (data is null)
            {
                return Result<VenueListResponseModel>.NotFoundError("No venue found.");
            }

            model.VenueList = data.Select(VenueListModel.FromTblVenue).ToList();
            return Result<VenueListResponseModel>.Success(model);
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            return Result<VenueListResponseModel>.SystemError(ex.Message);
        }
    }
    
    #endregion
    
    #region Get VenueById

    public async Task<Result<VenueEditResponseModel>> Edit(string venueId)
    {
        var model = new VenueEditResponseModel();
        if (venueId.IsNullOrEmpty())
        {
            return Result<VenueEditResponseModel>.ValidationError("Venue Id cannot be null or empty.");
        }
        
        try
        {
            var venue = await _db.TblVenues
                            .FirstOrDefaultAsync(
                                x => x.Venueid == venueId && 
                                x.Deleteflag == false
                            );
            if (venue is null)
            {
                return Result<VenueEditResponseModel>.NotFoundError("No venue found.");
            }

            model.Venue = VenueEditModel.FromTblVenue(venue);
            return Result<VenueEditResponseModel>.Success(model);
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            return Result<VenueEditResponseModel>.SystemError(ex.Message);
        }
    }
    
    #endregion

    #region Create Venue
    public async Task<Result<VenueCreateResponseModel>> Create(VenueCreateRequestModel requestModel)
    {
        try
        {
            var newVenue = new TblVenue()
            {
                Venueid = GenerateUlid(),
                Venuecode = await _commonService.GenerateSequenceCode(EnumTableUniqueName.Tbl_Venue),
                Venuetypecode = requestModel.VenueTypeCode,
                Venuename = requestModel.VenueName,
                Description = requestModel.Description,
                Address = requestModel.Address!,
                Capacity = requestModel.Capacity,
                Facilities = requestModel.Facilities,
                Addons = requestModel.Addons,
                Venueimage = requestModel.Image!,
                Createdby = CreatedByUserId,
                Createdat = DateTime.Now,
                Deleteflag = false
            };
            await _db.TblVenues.AddAsync(newVenue);
            await _db.SaveAndDetachAsync();

            return Result<VenueCreateResponseModel>.Success("Admin created successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            return Result<VenueCreateResponseModel>.SystemError(ex.Message);
        }
    }
    
    #endregion
    
    #region Update Venue

    public async Task<Result<VenueUpdateResponseModel>> Update(VenueUpdateRequestModel requestModel)
    {
        if (requestModel.VenueId.IsNullOrEmpty())
        {
            return Result<VenueUpdateResponseModel>.UserInputError("Venue Id cannot be null or empty.");
        }
        
        try
        {
            var existingVenue = await _db.TblVenues.FindAsync(requestModel.VenueId);

            if (existingVenue is null)
            {
                return Result<VenueUpdateResponseModel>.NotFoundError("No venue found.");
            }

            existingVenue.Venuetypecode = requestModel.VenueTypeCode;
            existingVenue.Venuename = requestModel.VenueName;
            existingVenue.Description = requestModel.Description;
            existingVenue.Address = requestModel.Address!;
            existingVenue.Capacity = requestModel.Capacity;
            existingVenue.Facilities = requestModel.Facilities;
            existingVenue.Addons = requestModel.Addons;
            existingVenue.Venueimage = requestModel.Image!;
            existingVenue.Modifiedby = CreatedByUserId;
            existingVenue.Modifiedat = DateTime.Now;

            _db.Entry(existingVenue).State = EntityState.Modified;
            await _db.SaveAndDetachAsync();

            return Result<VenueUpdateResponseModel>.Success("Venue updated successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            return Result<VenueUpdateResponseModel>.SystemError(ex.Message);
        }
    }
    
    #endregion
    
    #region Delete Venue

    public async Task<Result<VenueDeleteResponseModel>> Delete(string venueId)
    {
        if (venueId.IsNullOrEmpty())
        {
            return Result<VenueDeleteResponseModel>.UserInputError("Venue Id cannot be null or empty.");
        }
        
        try
        {
            var venue = await _db.TblVenues.FindAsync(venueId);

            if (venue is null)
            {
                return Result<VenueDeleteResponseModel>.NotFoundError("There is no venue with this venue id.");
            }

            venue.Deleteflag = true;
            venue.Modifiedby = CreatedByUserId;
            venue.Modifiedat = DateTime.Now;
            
            _db.Entry(venue).State = EntityState.Modified;
            await _db.SaveAndDetachAsync();

            return Result<VenueDeleteResponseModel>.Success("Venue deleted successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            return Result<VenueDeleteResponseModel>.SystemError(ex.Message);
        }
    }
    
    #endregion
}