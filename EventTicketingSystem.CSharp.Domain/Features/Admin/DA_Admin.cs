namespace EventTicketingSystem.CSharp.Domain.Features.Admin;

public class DA_Admin
{
    private readonly AppDbContext _db;
    private readonly ILogger<DA_Admin> _logger;
    private readonly CommonService _commonService;
    private const string CreatedByUserId = "Admin";

    public DA_Admin(ILogger<DA_Admin> logger, AppDbContext db, CommonService commonService)
    {
        _logger = logger;
        _db = db;
        _commonService = commonService;
    }

    #region Get Admin List

    public async Task<Result<AdminResponseModel>> GetAdminListAsync()
    {
        var responesModel = new Result<AdminResponseModel>();
        var model = new AdminResponseModel();

        try
        {
            var data = await _db.TblAdmins
                        .AsNoTracking()
                        .Where(x => x.Deleteflag == false)
                        .ToListAsync();
            if (data is null)
            {
                responesModel = Result<AdminResponseModel>.Success("No admin user found.");
            }

            model.AdminList = data.Select(AdminModel.FromTblAdmin).ToList();
            responesModel = Result<AdminResponseModel>.Success(model);
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            responesModel = Result<AdminResponseModel>.SystemError(ex.Message);
        }
        return responesModel;
    }

    #endregion

    #region Get AdminByCode

    public async Task<Result<AdminResponseModel>> GetAdminByCodeAsync(string adminCode)
    {
        var responseModel = new Result<AdminResponseModel>();
        var model = new AdminResponseModel();

        try
        {
            if (adminCode.IsNullOrEmpty())
            {
                return Result<AdminResponseModel>.ValidationError("Admin Code cannot be null or empty.");
            }

            var admin = await _db.TblAdmins
                        .FirstOrDefaultAsync(
                            x => x.Admincode == adminCode &&
                            x.Deleteflag == false
                        );
            if (admin is null)
            {
                return Result<AdminResponseModel>.NotFoundError("Admin Not Found.");
            }

            model.Admin = AdminModel.FromTblAdmin(admin);
            responseModel = Result<AdminResponseModel>.Success(model);
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            responseModel = Result<AdminResponseModel>.SystemError(ex.Message);
        }

        return responseModel;
    }

    #endregion

    #region Create Admin

    public async Task<Result<AdminResponseModel>> CreateAdminAsync(AdminRequestModel requestModel)
    {
        var responseModel = new Result<AdminResponseModel>();
        var model = new AdminResponseModel();

        if (requestModel.Username.IsNullOrEmpty())
        {
            responseModel = Result<AdminResponseModel>.ValidationError("Username cannot be empty!");
        }

        else if (requestModel.Email.IsNullOrEmpty())
        {
            responseModel = Result<AdminResponseModel>.ValidationError("Email Cannot be blank!");
        }

        else if (!requestModel.Email.IsValidEmail() || isEmailUsed(requestModel.Email))
        {
            responseModel = Result<AdminResponseModel>.ValidationError("Email is either already used or invalid!");
        }

        else if (requestModel.PhoneNo.IsNullOrEmpty() || requestModel.PhoneNo!.Length < 9)
        {
            responseModel = Result<AdminResponseModel>.ValidationError("Phone number cannot be empty or less than 9 numbers!");
        }

        else if (requestModel.Password.IsNullOrEmpty() || requestModel.Password!.Length < 8)
        {
            responseModel = Result<AdminResponseModel>.ValidationError("Password cannot be empty or must contain at least 8 characters");
        }

        else if (!requestModel.Password.Any(char.IsUpper))
        {
            responseModel = Result<AdminResponseModel>.ValidationError("Password must contain at least one upper letter!");
        }

        else if (!requestModel.Password.Any(char.IsLower))
        {
            responseModel = Result<AdminResponseModel>.ValidationError("Password must contain at least one lower letter!");
        }

        else if (!requestModel.Password.Any(char.IsDigit))
        {
            responseModel = Result<AdminResponseModel>.ValidationError("Password must contain at least one digit!");
        }

        else if (!requestModel.Password.Any(c => !char.IsLetterOrDigit(c)))
        {
            responseModel = Result<AdminResponseModel>.ValidationError("Password must contain at least one special character!");
        }

        else
        {
            try
            {
                var newAdmin = new TblAdmin()
                {
                    Adminid = Ulid.NewUlid().ToString(),
                    Admincode = await _commonService.GenerateSequenceCode(EnumTableUniqueName.Tbl_Admin),
                    Username = requestModel.Username,
                    Email = requestModel.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(requestModel.Password),
                    Phoneno = requestModel.PhoneNo,
                    Createdby = CreatedByUserId,
                    Createdat = DateTime.Now,
                    Deleteflag = false,
                };

                await _db.TblAdmins.AddAsync(newAdmin);
                await _db.SaveChangesAsync();

                model.Admin = AdminModel.FromTblAdmin(newAdmin);
                responseModel = Result<AdminResponseModel>.Success(model, "Admin was created Successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogExceptionError(ex);
                responseModel = Result<AdminResponseModel>.SystemError(ex.ToString());
            }
        }

        return responseModel;
    }

    #endregion

    #region Update Admin

    public async Task<Result<AdminResponseModel>> UpdateAdminAsync(string adminCode, AdminRequestModel requestModel)
    {
        var responseModel = new Result<AdminResponseModel>();
        var model = new AdminResponseModel();
        string errorMsg = string.Empty;

        var admin = await _db.TblAdmins
                            .FirstOrDefaultAsync(
                                x => x.Admincode == adminCode &&
                                x.Deleteflag == false
                            );
        if (admin is null)
        {
            return Result<AdminResponseModel>.NotFoundError("There is no admin with that admin code.");
        }

        else if (new[]
        {
            requestModel.Username,
            requestModel.Email,
            requestModel.PhoneNo,
            requestModel.Password
        }.Any(string.IsNullOrEmpty))
        {
            return Result<AdminResponseModel>.UserInputError("All fields are empty. Please provide at least one field to update.");
        }

        else
        {
            if (!requestModel.Username.IsNullOrEmpty())
            {
                admin.Username = requestModel.Username;
            }

            if (!requestModel.Email.IsNullOrEmpty())
            {
                if (!requestModel.Email.IsValidEmail() || isEmailUsed(requestModel.Email))
                {
                    errorMsg = "Email is either invalid or already used!";
                    goto ReturnErrorResult;
                }
                admin.Email = requestModel.Email;
            }
            if (!requestModel.PhoneNo.IsNullOrEmpty())
            {
                if (requestModel.PhoneNo.Length < 9)
                {
                    errorMsg = "Phone number must contain at least 9 numbers!";
                    goto ReturnErrorResult;
                }
                admin.Phoneno = requestModel.PhoneNo;

            }

            if (!requestModel.Password.IsNullOrEmpty())
            {
                if (requestModel.Password.Length < 8)
                {
                    errorMsg = "Password must contain at least 8 characters!";
                    goto ReturnErrorResult;
                }

                else if (!requestModel.Password.Any(char.IsUpper))
                {
                    errorMsg = "Password must contain at least one upper letter!";
                    goto ReturnErrorResult;
                }

                else if (!requestModel.Password.Any(char.IsLower))
                {
                    errorMsg = "Password must contain at least one lower letter!";
                    goto ReturnErrorResult;
                }

                else if (!requestModel.Password.Any(char.IsDigit))
                {
                    errorMsg = "Password must contain at least one digit!";
                    goto ReturnErrorResult;
                }
                else if (!requestModel.Password.Any(c => !char.IsLetterOrDigit(c)))
                {
                    errorMsg = "Password must contain at least one special character!";
                    goto ReturnErrorResult;
                }
                admin.Password = BCrypt.Net.BCrypt.HashPassword(requestModel.Password);
            }
            try
            {
                admin.Modifiedby = CreatedByUserId;
                admin.Modifiedat = DateTime.Now;

                _db.Entry(admin).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                model.Admin = AdminModel.FromTblAdmin(admin);
                responseModel = Result<AdminResponseModel>.Success(model, "Admin data was updated successufully!");
            }
            catch (Exception ex)
            {
                _logger.LogExceptionError(ex);
                responseModel = Result<AdminResponseModel>.SystemError(ex.ToString());
            }
        }

        return responseModel;

    ReturnErrorResult:

        return Result<AdminResponseModel>.ValidationError(errorMsg);
    }

    #endregion

    #region Delete Admin

    public async Task<Result<AdminResponseModel>> DeleteAdminByCode(string adminCode)
    {
        var responseModel = new Result<AdminResponseModel>();
        var model = new AdminResponseModel();

        if (adminCode.IsNullOrEmpty())
        {
            return Result<AdminResponseModel>.UserInputError("The admin code cannot be null or empty.");
        }

        var data = await _db.TblAdmins
                        .FirstOrDefaultAsync(
                            x => x.Admincode == adminCode &&
                            x.Deleteflag == false
                        );
        if (data is null)
        {
            return Result<AdminResponseModel>.NotFoundError("There is no admin with this admin code.");
        }

        try
        {
            data.Modifiedby = CreatedByUserId;
            data.Modifiedat = DateTime.Now;
            data.Deleteflag = true;
            _db.Entry(data).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            responseModel = Result<AdminResponseModel>.Success("Admin data is deleted successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            responseModel = Result<AdminResponseModel>.SystemError(ex.Message);
        }

        return responseModel;
    }

    #endregion

    #region Helper Function

    private bool isEmailUsed(string email)
    {
        var admin = _db.TblAdmins.AsNoTracking().FirstOrDefault(x => x.Email == email);
        if (admin is null) return false;
        return true;
    }

    //private int getLastIndex()
    //{
    //    var adminCode = _db.TblAdmins
    //                        .AsNoTracking()
    //                        .OrderByDescending(x => x.Createdat)
    //                        .Select(x => x.Admincode)
    //                        .FirstOrDefault();

    //    if (adminCode is null) return 1;

    //    var lastIndex = adminCode.Substring(2);
    //    return int.Parse(lastIndex) + 1;
    //}

    //private string generateAdminCode()
    //{
    //    string adminCode = "A" + getLastIndex().ToString("D4");
    //    return adminCode;
    //}

    #endregion  
}