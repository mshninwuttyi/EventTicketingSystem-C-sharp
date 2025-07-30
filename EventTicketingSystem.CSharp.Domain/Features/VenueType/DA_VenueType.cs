﻿namespace EventTicketingSystem.CSharp.Domain.Features.VenueType;

public class DA_VenueType : AuthorizationService
{
    private readonly ILogger<DA_VenueType> _logger;
    private readonly AppDbContext _db;
    private readonly CommonService _commonService;

    public DA_VenueType(IHttpContextAccessor httpContextAccessor,
                        ILogger<DA_VenueType> logger,
                        AppDbContext db,
                        CommonService commonService) : base(httpContextAccessor)
    {
        _logger = logger;
        _db = db;
        _commonService = commonService;
    }

    #region VenueType List

    public async Task<Result<VenueTypeListResponseModel>> List()
    {
        var model = new VenueTypeListResponseModel();
        try
        {
            var data = await _db.TblVenuetypes
                                .Where(x => x.Deleteflag == false)
                                .OrderByDescending(x => x.Venuetypeid)
                                .ToListAsync();
            if (data is null)
            {
                return Result<VenueTypeListResponseModel>.NotFoundError("Venue Type Not Found.");
            }

            model.VenueTypeList = data.Select(VenueTypeListModel.FromTblVenueType).ToList();
            return Result<VenueTypeListResponseModel>.Success(model);
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            return Result<VenueTypeListResponseModel>.SystemError(ex.Message);
        }
    }

    #endregion

    #region Venue Type Edit

    public async Task<Result<VenueTypeEditResponseModel>> Edit(string venueTypeCode)
    {
        var model = new VenueTypeEditResponseModel();
        if (venueTypeCode.IsNullOrEmpty())
        {
            return Result<VenueTypeEditResponseModel>.UserInputError("Event Type Not Found.");
        }
        try
        {
            var item = await _db.TblVenuetypes
                                        .FirstOrDefaultAsync(
                                            x => x.Venuetypecode == venueTypeCode &&
                                            x.Deleteflag == false
                                        );
            if (item is null)
            {
                return Result<VenueTypeEditResponseModel>.NotFoundError("Event Type Not Found.");
            }

            model.VenueTypeEdit = VenueTypeEditModel.FromTblVenueType(item);
            return Result<VenueTypeEditResponseModel>.Success(model);
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            return Result<VenueTypeEditResponseModel>.SystemError(ex.Message);
        }
    }

    #endregion

    #region Venue Type Create

    public async Task<Result<VenueTypeCreateResponseModel>> Create(VenueTypeCreateRequestModel requestModel)
    {
        if (requestModel.VenueTypeName.IsNullOrEmpty())
        {
            return Result<VenueTypeCreateResponseModel>.ValidationError("Venue Type Name Required.");
        }
        else if (isVenueTypeNameExist(requestModel.VenueTypeName))
        {
            return Result<VenueTypeCreateResponseModel>.ValidationError("Venue Type Name already exist.");
        }
        else
        {
            try
            {
                var newVenueType = new TblVenuetype
                {
                    Venuetypeid = Ulid.NewUlid().ToString(),
                    Venuetypecode = await _commonService.GenerateSequenceCode(EnumTableUniqueName.Tbl_VenueType),
                    Venuetypename = requestModel.VenueTypeName,
                    Createdat = DateTime.Now,
                    Createdby = CurrentUserId,
                    Deleteflag = false
                };
                await _db.TblVenuetypes.AddAsync(newVenueType);
                await _db.SaveAndDetachAsync();

                return Result<VenueTypeCreateResponseModel>.Success("Venue Type Created Successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogExceptionError(ex);
                return Result<VenueTypeCreateResponseModel>.SystemError(ex.Message);
            }
        }
    }

    #endregion

    #region Venue Type Update

    public async Task<Result<VenueTypeUpdateResponseModel>> Update(VenueTypeUpdateRequestModel requestModel)
    {
        if (requestModel.VenueTypeCode.IsNullOrEmpty())
        {
            return Result<VenueTypeUpdateResponseModel>.ValidationError("Venue Type Code Not Found.");
        }
        else if (requestModel.VenueTypeName.IsNullOrEmpty())
        {
            return Result<VenueTypeUpdateResponseModel>.ValidationError("Venue Type Name Required.");
        }
        else
        {
            try
            {
                var existingVenueType = await _db.TblVenuetypes
                                        .FirstOrDefaultAsync(
                                            x => x.Venuetypecode == requestModel.VenueTypeCode &&
                                            x.Deleteflag == false
                                        );
                if (existingVenueType is null)
                {
                    return Result<VenueTypeUpdateResponseModel>.NotFoundError("Venue Type Name Not Found");
                }

                existingVenueType.Venuetypename = requestModel.VenueTypeName;
                existingVenueType.Modifiedby = CurrentUserId;
                existingVenueType.Modifiedat = DateTime.Now;
                _db.Entry(existingVenueType).State = EntityState.Modified;
                await _db.SaveAndDetachAsync();

                return Result<VenueTypeUpdateResponseModel>.Success("Venue Type Updated Successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogExceptionError(ex);
                return Result<VenueTypeUpdateResponseModel>.SystemError(ex.Message);
            }
        }
    }

    #endregion

    #region Venue Type Delete

    public async Task<Result<VenueTypeDeleteResponseModel>> Delete(string venueTypeCode)
    {
        if (venueTypeCode.IsNullOrEmpty())
        {
            return Result<VenueTypeDeleteResponseModel>.UserInputError("Venue Type Not Found.");
        }

        try
        {
            var item = await _db.TblVenuetypes
                        .FirstOrDefaultAsync(
                            x => x.Venuetypecode == venueTypeCode &&
                            x.Deleteflag == false
                        );
            if (item is null)
            {
                return Result<VenueTypeDeleteResponseModel>.NotFoundError("Venue Type Not Found.");
            }

            item.Deleteflag = true;
            item.Modifiedby = CurrentUserId;
            item.Modifiedat = DateTime.Now;
            _db.Entry(item).State = EntityState.Modified;
            await _db.SaveAndDetachAsync();

            return Result<VenueTypeDeleteResponseModel>.Success("Venue Type Deleted Successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            return Result<VenueTypeDeleteResponseModel>.SystemError(ex.Message);
        }
    }

    #endregion

    #region Private function

    private bool isVenueTypeNameExist(string VenueTypeName)
    {
        return _db.TblVenuetypes
            .AsEnumerable()
            .Any(x => string.Equals(x.Venuetypename, VenueTypeName, StringComparison.OrdinalIgnoreCase));
    }

    #endregion
}