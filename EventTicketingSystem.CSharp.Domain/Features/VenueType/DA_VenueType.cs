using EventTicketingSystem.CSharp.Domain.Models.Features.VenueType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTicketingSystem.CSharp.Domain.Features.VenueType
{
    public class DA_VenueType
    {
        private readonly ILogger<DA_VenueType> _logger;
        private readonly AppDbContext _db;
        private readonly CommonService _commonService;
        private const string CreatedByUserId = "Admin";

        public DA_VenueType(ILogger<DA_VenueType> logger, AppDbContext db, CommonService commonService)
        {
            _logger = logger;
            _db = db;
            _commonService = commonService;
        }

        #region Get Venue Type List

        public async Task<Result<VenueTypeResponseModel>> GetVenueTypeList()
        {
            var responseModel = new Result<VenueTypeResponseModel>();
            var model = new VenueTypeResponseModel();
            try
            {
                var data = await _db.TblVenuetypes
                                    .Where(x => x.Deleteflag == false)
                                    .ToListAsync();
                if (data is null)
                {
                    return Result<VenueTypeResponseModel>.NotFoundError("No Venue Type Found!");
                }

                model.VenueTypes = data.Select(x => new VenueTypeModel
                {
                    Venuetypeid = x.Venuetypeid,
                    Venuetypecode = x.Venuetypecode,
                    Venuetypename = x.Venuetypename,
                    Createdby = x.Createdby,
                    Createdat = x.Createdat,
                    Modifiedby = x.Modifiedby,
                    Modifiedat = x.Modifiedat,
                    Deleteflag = false

                }).ToList();

                return Result<VenueTypeResponseModel>.Success(model);

            }
            catch (Exception ex)
            {
                _logger.LogExceptionError(ex);
                return Result<VenueTypeResponseModel>.SystemError(ex.Message);
            }
        }

        #endregion

        #region create Venue Type

        public async Task<Result<CreateVenueTypeResponseModel>> CreateVenueType(VenueTypeRequestModel request)
        {
            if (request.VenueTypeName.IsNullOrEmpty())
            {
                return Result<CreateVenueTypeResponseModel>.ValidationError("Fill up Venue Type Name!");
            }
            else if (isVenueTypeNameExist(request.VenueTypeName))
            {
                return Result<CreateVenueTypeResponseModel>.ValidationError("Venue Type Name already exist!");
            }
            else
            {
                try
                {
                    var newVenueType = new TblVenuetype
                    {
                        Venuetypeid = GenerateUlid(),
                        Venuetypecode = await _commonService.GenerateSequenceCode(EnumTableUniqueName.Tbl_VenueType),
                        Venuetypename = request.VenueTypeName,
                        Createdat = DateTime.Now,
                        Createdby = CreatedByUserId,
                        Deleteflag = false
                    };
                    var entry = await _db.TblVenuetypes.AddAsync(newVenueType);
                    await _db.SaveAndDetachAsync();

                    var createdEventCategory = entry.Entity;
                    var model = new CreateVenueTypeResponseModel
                    {
                        Venuetypeid = createdEventCategory.Venuetypeid,
                        Venuetypecode = createdEventCategory.Venuetypecode,
                        Venuetypename = createdEventCategory.Venuetypename,
                        Createdat = createdEventCategory.Createdat,
                        Createdby = createdEventCategory.Createdby
                    };

                    return Result<CreateVenueTypeResponseModel>.Success(model, "New Venue Type Created");
                }
                catch (Exception ex)
                {
                    _logger.LogExceptionError(ex);
                    return Result<CreateVenueTypeResponseModel>.SystemError(ex.Message);
                }
            }
        }

        #endregion

        #region Update Venue Type

        public async Task<Result<UpdateVenueTypeResponseModel>> UpdateVenueType(VenueTypeRequestModel request)
        {

            if (request.AdminCode.IsNullOrEmpty())
            {
                return Result<UpdateVenueTypeResponseModel>.ValidationError("Admin not found");
            }
            else if (request.VenueTypeName.IsNullOrEmpty())
            {
                return Result<UpdateVenueTypeResponseModel>.ValidationError("Venue Type name not found");
            }
            else
            {
                try
                {
                    var existingVenueType = await _db.TblVenuetypes
                                            .FirstOrDefaultAsync(
                                                x => x.Venuetypecode == request.VenueTypeCode &&
                                                x.Deleteflag == false
                                            );
                    if (existingVenueType is null)
                    {
                        return Result<UpdateVenueTypeResponseModel>.NotFoundError("Venue Type Name not found");
                    }

                    existingVenueType.Venuetypename = request.VenueTypeName;
                    existingVenueType.Modifiedby = CreatedByUserId;
                    existingVenueType.Modifiedat = DateTime.Now;
                    var entry = _db.TblVenuetypes.Update(existingVenueType);
                    await _db.SaveAndDetachAsync();

                    var updateVenueType = entry.Entity;
                    var model = new UpdateVenueTypeResponseModel
                    {
                        Venuetypeid = updateVenueType.Venuetypeid,
                        Venuetypecode = updateVenueType.Venuetypecode,
                        Venuetypename = updateVenueType.Venuetypename,
                        Modifiedat = updateVenueType.Modifiedat,
                        Modifiedby = updateVenueType.Modifiedby
                    };

                    return Result<UpdateVenueTypeResponseModel>.Success(model, "Venue Type Name updated successfully");
                }
                catch (Exception ex)
                {
                    _logger.LogExceptionError(ex);
                    return Result<UpdateVenueTypeResponseModel>.SystemError(ex.Message);
                }
            }
        }

        #endregion

        #region Delete Venue Type

        public async Task<Result<VenueTypeResponseModel>> DeleteVenueType(string? venueTypeCode)
        {
            if (venueTypeCode.IsNullOrEmpty())
            {
                return Result<VenueTypeResponseModel>.UserInputError("Venue Type Code can't be Null or Empty!");
            }

            try
            {
                var data = await _db.TblVenuetypes
                            .FirstOrDefaultAsync(
                                x => x.Venuetypecode == venueTypeCode &&
                                x.Deleteflag == false
                            );
                if (data is null)
                {
                    return Result<VenueTypeResponseModel>.NotFoundError($"No Venue Type Found with Code: {venueTypeCode}");
                }

                data.Deleteflag = true;
                data.Modifiedby = CreatedByUserId;
                data.Modifiedat = DateTime.Now;
                _db.Entry(data).State = EntityState.Modified;
                await _db.SaveAndDetachAsync();

                return Result<VenueTypeResponseModel>.Success("Venue Type Deleted Successfully!");
            }
            catch (Exception ex)
            {
                _logger.LogExceptionError(ex);
                return Result<VenueTypeResponseModel>.SystemError(ex.Message);
            }
        }

        #endregion

        #region Private function

        private bool isVenueTypeNameExist(string? venueTypeName)
        {

            return _db.TblVenuetypes
        .AsEnumerable()
        .Any(x => string.Equals(x.Venuetypename, venueTypeName, StringComparison.OrdinalIgnoreCase));
        }


        #endregion
    }

}

