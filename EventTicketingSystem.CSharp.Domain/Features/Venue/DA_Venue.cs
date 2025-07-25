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
                return Result<VenueListResponseModel>.NotFoundError("Venue Not Found.");
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

    public async Task<Result<VenueEditResponseModel>> Edit(string venueId)
    {
        var model = new VenueEditResponseModel();
        try
        {
            var venue = await _db.TblVenues
                            .FirstOrDefaultAsync(
                                x => x.Venueid == venueId && 
                                x.Deleteflag == false
                            );
            if (venue is null)
            {
                return Result<VenueEditResponseModel>.NotFoundError("Venue not found.");
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

    public async Task<Result<VenueCreateResponseModel>> Create(VenueCreateRequestModel requestModel)
    {
        try
        {
            var venueEntity = new TblVenue()
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
            await _db.TblVenues.AddAsync(venueEntity);
            await _db.SaveAndDetachAsync();

            return Result<VenueCreateResponseModel>.Success("Venue created successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            return Result<VenueCreateResponseModel>.SystemError(ex.Message);
        }
    }

    public async Task<Result<VenueUpdateResponseModel>> Update(VenueUpdateRequestModel requestModel)
    {
        try
        {
            var existingVenue = await _db.TblVenues.FindAsync(requestModel.VenueId);

            if (existingVenue is null)
            {
                return Result<VenueUpdateResponseModel>.NotFoundError("Venue not found.");
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

    public async Task<Result<VenueDeleteResponseModel>> Delete(string venueId)
    {
        try
        {
            var venue = await _db.TblVenues.FindAsync(venueId);

            if (venue is null)
            {
                return Result<VenueDeleteResponseModel>.NotFoundError("Venue not found");
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
}