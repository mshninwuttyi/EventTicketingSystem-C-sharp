namespace EventTicketingSystem.CSharp.Domain.Features.BusinessOwner;

public class DA_BusinessOwner
{
    private readonly ILogger<DA_BusinessOwner> _logger;
    private readonly AppDbContext _db;
    private readonly CommonService _commonService;
    private const string CreatedByUserId = "Admin";

    public DA_BusinessOwner(ILogger<DA_BusinessOwner> logger, AppDbContext db, CommonService commonService)
    {
        _logger = logger;
        _db = db;
        _commonService = commonService;
    }

    #region Private Helper Functions
    private int getTotalOwnerCount()
    {
        return _db.TblBusinessowners.Count();
    }
    private bool isOwnerCodeExist(String ownerCode)
    {
        var owner = _db.TblBusinessowners.AsNoTracking().FirstOrDefault(x => x.Businessownercode == ownerCode && x.Deleteflag == true);
        return owner == null;
    }
    private bool isEmailUsed(String email)
    {
        var owner = _db.TblBusinessowners.AsNoTracking().FirstOrDefault(x => x.Email == email);
        return owner == null;
    }
    private int getLastestCount()
    {
        int count = 0;

        var result = _db.TblBusinessowners.AsNoTracking().OrderByDescending(x => x.Createdat).Select(x => x.Businessownercode).FirstOrDefault();
        if (result != null)
        {
            count = getIntFromCode(result);
        }
        return count;
    }
    private int getIntFromCode(string code)
    {
        string noString = code.Substring(3, code.Length - 1);
        return int.Parse(noString);
    }
    #endregion

    #region Get Business Owner List

    public async Task<Result<BusinessOwnerResponseModel>> GetList()
    {
        var responseModel = new Result<BusinessOwnerResponseModel>();
        var model = new BusinessOwnerResponseModel();
        try
        {
            var data = await _db.TblBusinessowners
                        .AsNoTracking()
                        .Where(x => x.Deleteflag == false)
                        .ToListAsync();
            if (data is null)
            {
                return Result<BusinessOwnerResponseModel>.NotFoundError("No Owner Found.");
            }

            model.BusinessOwners = data.Select(x => new BusinessOwnerModel
            {
                Businessownerid = x.Businessownerid,
                Businessownercode = x.Businessownercode,
                FullName = x.Fullname,
                Email = x.Email,
                Phone = x.Phone,
                Createdby = x.Createdby,
                Createdat = x.Createdat,
                Modifiedby = x.Modifiedby,
                Modifiedat = x.Modifiedat,
                Deleteflag = false
            }).ToList();

            responseModel = Result<BusinessOwnerResponseModel>.Success(model);
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            responseModel = Result<BusinessOwnerResponseModel>.SystemError(ex.Message);
        }

        return responseModel;
    }

    #endregion

    #region Get Business Owner By Code

    public async Task<Result<BusinessOwnerResponseModel>> GetByCode(string? ownerCode)
    {
        var responseModel = new Result<BusinessOwnerResponseModel>();
        var model = new BusinessOwnerResponseModel();

        if (ownerCode.IsNullOrEmpty())
        {
            return Result<BusinessOwnerResponseModel>.UserInputError("Owner Code can't be Null or Empty!");
        }

        try
        {
            var data = await _db.TblBusinessowners
                        .FirstOrDefaultAsync(
                            x => x.Businessownercode == ownerCode &&
                            x.Deleteflag == false
                        );
            if (data is null)
            {
                responseModel = Result<BusinessOwnerResponseModel>.NotFoundError($"No Owner Found with Code: {ownerCode}");
            }

            model.BusinessOwner = new BusinessOwnerModel
            {
                Businessownerid = data.Businessownerid,
                Businessownercode = data.Businessownercode,
                FullName = data.Fullname,
                Email = data.Email,
                Phone = data.Phone,
                Createdby = data.Createdby,
                Createdat = data.Createdat,
                Modifiedby = data.Modifiedby,
                Modifiedat = data.Modifiedat,
                Deleteflag = false
            };
            responseModel = Result<BusinessOwnerResponseModel>.Success(model);
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            responseModel = Result<BusinessOwnerResponseModel>.SystemError(ex.Message);
        }

        return responseModel;
    }

    #endregion

    #region Create Business Owner

    public async Task<Result<BusinessOwnerResponseModel>> CreateBusinessOwner(BusinessOwnerRequestModel owner)
    {
        var responseModel = new Result<BusinessOwnerResponseModel>();
        var model = new BusinessOwnerResponseModel();

        if (owner.Email.IsNullOrEmpty() || !owner.Email.IsValidEmail() || isEmailUsed(owner.Email))
        {
            return Result<BusinessOwnerResponseModel>.ValidationError("Email is already used or invalid!");
        }
        else if (owner.FullName.IsNullOrEmpty() || owner.FullName.Length < 3)
        {
            return Result<BusinessOwnerResponseModel>.ValidationError("Name can't be blank or less than 3 characters.");
        }
        else if (owner.Phone.IsNullOrEmpty() || owner.Phone.Length < 9)
        {
            return Result<BusinessOwnerResponseModel>.ValidationError("Phone No can't be empty or less than 9 digits!");
        }
        else
        {
            try
            {
                var newOwner = new TblBusinessowner()
                {
                    Businessownerid = Ulid.NewUlid().ToString(),
                    Businessownercode = await _commonService.GenerateSequenceCode(EnumTableUniqueName.Tbl_BusinessOwner),
                    Fullname = owner.FullName,
                    Email = owner.Email,
                    Phone = owner.Phone,
                    Createdby = CreatedByUserId,
                    Createdat = DateTime.Now,
                    Deleteflag = false
                };
                await _db.TblBusinessowners.AddAsync(newOwner);
                await _db.SaveAndDetachAsync();

                model.BusinessOwner = BusinessOwnerModel.FromTblOwner(newOwner);
                responseModel = Result<BusinessOwnerResponseModel>.Success(model, "New Owner Created!");
            }
            catch (Exception ex)
            {
                _logger.LogExceptionError(ex);
                responseModel = Result<BusinessOwnerResponseModel>.SystemError(ex.Message);
            }
        }

        return responseModel;
    }

    #endregion

    #region Update Business Owner

    public async Task<Result<BusinessOwnerResponseModel>> UpdateBusinessOwner(BusinessOwnerRequestModel owner)
    {
        var responseModel = new Result<BusinessOwnerResponseModel>();
        var model = new BusinessOwnerResponseModel();
        string errorMessage = string.Empty;

        if (owner.Businessownercode.IsNullOrEmpty() || owner.Admin.IsNullOrEmpty())
        {
            return Result<BusinessOwnerResponseModel>.NotFoundError("Owner Code or Admin can't be empty!");
        }

        try
        {
            var data = await _db.TblBusinessowners
                        .FirstOrDefaultAsync(
                            x => x.Businessownercode == owner.Businessownercode &&
                            x.Deleteflag == false
                        );
            if (data is null)
            {
                return Result<BusinessOwnerResponseModel>.NotFoundError($"Owner not found with Code: {owner.Businessownercode}");
            }

            if (!owner.Email.IsNullOrEmpty() && owner.Email != data.Email)
            {
                if (owner.Email.IsValidEmail())
                {
                    if (!isEmailUsed(owner.Email))
                    {
                        data.Email = owner.Email;
                    }
                    else
                    {
                        errorMessage += "Email is already in use.\n";
                    }
                }
                else
                {
                    errorMessage += "Email is invalid!\n";
                }
            }

            if (!owner.FullName.IsNullOrEmpty() && data.Fullname != owner.FullName)
            {
                if (owner.FullName.Length >= 3)
                {
                    data.Fullname = owner.FullName;
                }
                else
                {
                    errorMessage += "Name is invalid.\n";
                }
            }

            if (!owner.Phone.IsNullOrEmpty() && data.Phone != owner.Phone)
            {
                if (owner.Phone.Length >= 9 && owner.Phone.Length < 11)
                {
                    data.Phone = owner.Phone;
                }
                else
                {
                    errorMessage += "Phone No is invalid!\n";
                }
            }

            data.Modifiedby = CreatedByUserId;
            data.Modifiedat = DateTime.Now;

            _db.Entry(data).State = EntityState.Modified;
            await _db.SaveAndDetachAsync();

            model.BusinessOwner = BusinessOwnerModel.FromTblOwner(data);
            if (errorMessage.IsNullOrEmpty())
            {
                responseModel = Result<BusinessOwnerResponseModel>.Success(model);
            }
            else
            {
                responseModel = Result<BusinessOwnerResponseModel>.ValidationError(errorMessage, model);
            }

        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            responseModel = Result<BusinessOwnerResponseModel>.SystemError(ex.Message);
        }

        return responseModel;
    }

    #endregion

    #region Delete Business Owner By Code

    public async Task<Result<BusinessOwnerResponseModel>> DeleteOwnerByCode(string? ownerCode)
    {
        var responseModel = new Result<BusinessOwnerResponseModel>();
        var model = new BusinessOwnerResponseModel();

        if (ownerCode.IsNullOrEmpty())
        {
            return Result<BusinessOwnerResponseModel>.UserInputError("Owner Code can't be Null or Empty!");
        }

        try
        {
            var data = await _db.TblBusinessowners
                        .FirstOrDefaultAsync(
                            x => x.Businessownercode == ownerCode &&
                            x.Deleteflag == false
                        );

            if (data != null)
            {
                responseModel = Result<BusinessOwnerResponseModel>.NotFoundError($"No Owner Found with Code: {ownerCode}");
            }

            data.Modifiedby = CreatedByUserId;
            data.Modifiedat = DateTime.Now;
            data.Deleteflag = true;
            _db.Entry(data).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            responseModel = Result<BusinessOwnerResponseModel>.Success(model, "Owner Deleted Successfully!");
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            responseModel = Result<BusinessOwnerResponseModel>.SystemError(ex.Message);
        }

        return responseModel;
    }

    #endregion
}