namespace EventTicketingSystem.CSharp.Domain.Features.BusinessOwner;

public class DA_BusinessOwner
{
    private readonly ILogger<DA_BusinessOwner> _logger;
    private readonly AppDbContext _db;

    public DA_BusinessOwner(ILogger<DA_BusinessOwner> logger, AppDbContext db)
    {
        _logger = logger;
        _db = db;
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

            model.BusinessOwners = data.Select(x => new BusinessOwnerModel
            {
                Businessownerid = x.Businessownerid,
                Businessownercode = x.Businessownercode,
                Name = x.Name,
                Email = x.Email,
                Phonenumber = x.Phonenumber,
                Createdby = x.Createdby,
                Createdat = x.Createdat,
                Modifiedby = x.Modifiedby,
                Modifiedat = x.Modifiedat,
                Deleteflag = false
            }).ToList();

            responseModel = Result<BusinessOwnerResponseModel>.Success(model, "Here are the business owners!");
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
            responseModel = Result<BusinessOwnerResponseModel>.UserInputError("Owner Code can't be Null or Empty!");
            goto ResultReturn;
        }
        try
        {
            var data = await _db.TblBusinessowners
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.Businessownercode == ownerCode && x.Deleteflag == false);

            if (data != null)
            {
                model.BusinessOwner = new BusinessOwnerModel
                {
                    Businessownerid = data.Businessownerid,
                    Businessownercode = data.Businessownercode,
                    Name = data.Name,
                    Email = data.Email,
                    Phonenumber = data.Phonenumber,
                    Createdby = data.Createdby,
                    Createdat = data.Createdat,
                    Modifiedby = data.Modifiedby,
                    Modifiedat = data.Modifiedat,
                    Deleteflag = false
                };
                responseModel = Result<BusinessOwnerResponseModel>.Success(model, "Here is the business Owner!");
            }
            else
            {
                responseModel = Result<BusinessOwnerResponseModel>.NotFoundError($"No Owner Found with Code: {ownerCode}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            responseModel = Result<BusinessOwnerResponseModel>.SystemError(ex.Message);
        }
    ResultReturn:
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
            responseModel = Result<BusinessOwnerResponseModel>.ValidationError("Email is already used or invalid!");
            goto ResultReturn;
        }
        else if (owner.Name.IsNullOrEmpty() || owner.Name.Length < 3)
        {
            responseModel = Result<BusinessOwnerResponseModel>.ValidationError("Name can't be blank or less than 3 characters.");
            goto ResultReturn;
        }
        else if (owner.Phonenumber.IsNullOrEmpty() || owner.Phonenumber.Length < 9)
        {
            responseModel = Result<BusinessOwnerResponseModel>.ValidationError("Phone No can't be empty or less than 9 digits!");
            goto ResultReturn;
        }
        else
        {
            try
            {
                int lastNumber = getLastestCount();
                lastNumber++;

                var newOwner = new TblBusinessowner()
                {
                    Businessownerid = Ulid.NewUlid().ToString(),
                    Businessownercode = "BO" + lastNumber.ToString("D4"),
                    Name = owner.Name,
                    Email = owner.Email,
                    Phonenumber = owner.Phonenumber,
                    Createdby = owner.Admin,
                    Createdat = DateTime.Now,
                    Deleteflag = false
                };
                _db.TblBusinessowners.Add(newOwner);
                await _db.SaveChangesAsync();

                model.BusinessOwner = BusinessOwnerModel.FromTblOwner(newOwner);
                responseModel = Result<BusinessOwnerResponseModel>.Success(model, "New Owner Created!");
            }
            catch (Exception ex)
            {
                _logger.LogExceptionError(ex);
                responseModel = Result<BusinessOwnerResponseModel>.SystemError(ex.Message);
            }
        }

    ResultReturn:
        return responseModel;
    }
    #endregion

    #region Update Business Owner
    public async Task<Result<BusinessOwnerResponseModel>> UpdateBusinessOwner(BusinessOwnerRequestModel owner)
    {
        var responseModel = new Result<BusinessOwnerResponseModel>();
        var model = new BusinessOwnerResponseModel();

        try
        {
            if (owner.Businessownercode.IsNullOrEmpty() || owner.Admin.IsNullOrEmpty())
            {
                responseModel = Result<BusinessOwnerResponseModel>.NotFoundError("Owner Code or Admin can't be empty!");
                goto ResultReturn;
            }
            else
            {
                var data = await _db.TblBusinessowners.AsNoTracking().Where(x => x.Businessownercode == owner.Businessownercode).FirstOrDefaultAsync();
                if (data == null)
                {
                    responseModel = Result<BusinessOwnerResponseModel>.NotFoundError($"Owner not found with Code: {owner.Businessownercode}");
                }
                else
                {
                    string errorMessage = "";
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

                    if (!owner.Name.IsNullOrEmpty() && data.Name != owner.Name)
                    {
                        if (owner.Name.Length >= 3)
                        {
                            data.Name = owner.Name;
                        }
                        else
                        {
                            errorMessage += "Name is invalid.\n";
                        }
                    }

                    if (!owner.Phonenumber.IsNullOrEmpty() && data.Phonenumber != owner.Phonenumber)
                    {
                        if (owner.Phonenumber.Length >= 9 && owner.Phonenumber.Length < 11)
                        {
                            data.Phonenumber = owner.Phonenumber;
                        }
                        else
                        {
                            errorMessage += "Phone No is invalid!\n";
                        }
                    }

                    data.Modifiedby = owner.Admin;
                    data.Modifiedat = DateTime.Now;

                    _db.Entry(data).State = EntityState.Modified;
                    await _db.SaveChangesAsync();

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

            }
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            responseModel = Result<BusinessOwnerResponseModel>.SystemError(ex.Message);
        }
    ResultReturn:
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
            responseModel = Result<BusinessOwnerResponseModel>.UserInputError("Owner Code can't be Null or Empty!");
            goto ResultReturn;
        }
        try
        {
            var data = await _db.TblBusinessowners
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.Businessownercode == ownerCode && x.Deleteflag == false);

            if (data != null)
            {
                data.Deleteflag = true;
                _db.Entry(data).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                responseModel = Result<BusinessOwnerResponseModel>.Success(model, "Owner Deleted Successfully!");
            }
            else
            {
                responseModel = Result<BusinessOwnerResponseModel>.NotFoundError($"No Owner Found with Code: {ownerCode}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            responseModel = Result<BusinessOwnerResponseModel>.SystemError(ex.Message);
        }
    ResultReturn:
        return responseModel;
    }
    #endregion
}