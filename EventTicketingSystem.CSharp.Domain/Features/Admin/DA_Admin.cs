using EventTicketingSystem.CSharp.Domain.Models.Features.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing.QrCode.Internal;

namespace EventTicketingSystem.CSharp.Domain.Features.Admin;

public class DA_Admin
{
    private readonly AppDbContext _db;
    private readonly ILogger<DA_Admin> _logger;

    public DA_Admin(ILogger<DA_Admin> logger, AppDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    #region GetAdminList
    public async Task<Result<AdminResponseModel>> GetAdminListAsync()
    {
        var responesModel = new Result<AdminResponseModel>();
        var model = new AdminResponseModel();

        var data = await _db.TblAdmins
            .AsNoTracking()
            .Where(x => x.Deleteflag == false)
            .ToListAsync();
        try
        {
            model.Admins = data.Select(AdminModel.FromTblAdmin).ToList();
            responesModel = Result<AdminResponseModel>.Success(model, "Admins are retrieved.");
        } 
        catch (Exception ex) 
        {
            _logger.LogExceptionError(ex);
            responesModel = Result<AdminResponseModel>.SystemError(ex.Message);
        }
        return responesModel;
    }
    #endregion

    #region GetAdminByCode
    public async Task<Result<AdminResponseModel>> GetAdminByCodeAsync(string code)
    {
        var responseModel = new Result<AdminResponseModel>();
        var model = new AdminResponseModel();

        if (string.IsNullOrEmpty(code))
        {
            responseModel = Result<AdminResponseModel>.UserInputError("Admin Code cannot be null or empty.");
            goto ReturnResult;
        }

        var admin = await _db.TblAdmins
                            .AsNoTracking()
                            .Where(x => x.Deleteflag == false)
                            .FirstOrDefaultAsync(x => x.Admincode == code);
        if (admin is null)
        {
            responseModel = Result<AdminResponseModel>.NotFoundError("There is no admin with that admin code.");
            goto ReturnResult;
        }

        try
        {
            model.Admin = AdminModel.FromTblAdmin(admin);
            responseModel = Result<AdminResponseModel>.Success(model, "Admin is retrieved successfully.");
        } 
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            responseModel = Result<AdminResponseModel>.SystemError(ex.Message);
        }

       ReturnResult:
        return responseModel;
    }
    #endregion

    #region CreateAdmin
    public async Task<Result<AdminResponseModel>> CreateAdminAsync(AdminRequestModel requestModel)
    {
        var responseModel = new Result<AdminResponseModel>();
        var model = new AdminResponseModel();

        if (string.IsNullOrEmpty(requestModel.Username))
        {
            responseModel = Result<AdminResponseModel>.ValidationError("Username cannot be empty!");
        }

        else if (string.IsNullOrEmpty(requestModel.Email))
        {
            responseModel = Result<AdminResponseModel>.ValidationError("Email Cannot be blank!");
        }

        else if (!requestModel.Email.IsValidEmail() || isEmailUsed(requestModel.Email))
        {
            responseModel = Result<AdminResponseModel>.ValidationError("Email is either already used or invalid!");
        }
        
        else if (string.IsNullOrEmpty(requestModel.PhoneNo) || requestModel.PhoneNo.Length<9)
        {
            responseModel = Result<AdminResponseModel>.ValidationError("Phone number cannot be empty or less than 9 numbers!");
        }

        else if (string.IsNullOrEmpty(requestModel.Password) || requestModel.Password.Length < 8)
        {
            responseModel = Result<AdminResponseModel>.ValidationError("Password cannot be empty or must contain at least 8 characters");
        }

        else if(!requestModel.Password.Any(char.IsUpper))
        {
            responseModel = Result<AdminResponseModel>.ValidationError("Password must contain at least one upper letter!");
        }

        else if(!requestModel.Password.Any(char.IsLower))
        {
            responseModel = Result<AdminResponseModel>.ValidationError("Password must contain at least one lower letter!");
        }

        else if (!requestModel.Password.Any(char.IsDigit))
        {
            responseModel = Result<AdminResponseModel>.ValidationError("Password must contain at least one digit!");
        }

        else if (!requestModel.Password.Any(c=>!char.IsLetterOrDigit(c)))
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
                    Admincode = generateAdminCode(),
                    Username = requestModel.Username,
                    Email = requestModel.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(requestModel.Password),
                    Phoneno = requestModel.PhoneNo,
                    Createdby = requestModel.Admin,
                    Createdat = DateTime.Now,
                    Deleteflag = false,
                };

                _db.TblAdmins.Add(newAdmin);
                int result = await _db.SaveChangesAsync();

                model.Admin = AdminModel.FromTblAdmin(newAdmin);
                responseModel = Result<AdminResponseModel>.Success(model, "Admin was created Successfully.");
                //goto ReturnResult;
            } 
            catch (Exception ex)
            {
                _logger.LogExceptionError(ex);
                responseModel = Result<AdminResponseModel>.SystemError(ex.ToString());
            }
        }

     ReturnResult:
        return responseModel;
    }
    #endregion

    #region UpdateAdmin
    public async Task<Result<AdminResponseModel>> UpdateAdminAsync(string adminCode, AdminRequestModel requestModel)
    {
        var responseModel = new Result<AdminResponseModel>();
        var model = new AdminResponseModel();

        var admin = await _db.TblAdmins
                            .AsNoTracking()
                            .Where(x => x.Deleteflag == false)
                            .FirstOrDefaultAsync(x => x.Admincode == adminCode);

        string errorMsg = string.Empty;

        if (admin == null)
        {
            responseModel = Result<AdminResponseModel>.NotFoundError("There is no admin with that admin code.");
        }

        else if(string.IsNullOrEmpty(requestModel.Username) && string.IsNullOrEmpty(requestModel.Email) && string.IsNullOrEmpty(requestModel.PhoneNo) && string.IsNullOrEmpty(requestModel.Password))
        {
            responseModel = Result<AdminResponseModel>.UserInputError("There is no data to update!");
        }

        else
        {
            if (!string.IsNullOrEmpty(requestModel.Username)) admin.Username = requestModel.Username;

            if (!string.IsNullOrEmpty(requestModel.Email))
            {
                if (!requestModel.Email.IsValidEmail() || isEmailUsed(requestModel.Email))
                {
                    errorMsg = "Email is either invalid or already used!";
                    goto ReturnErrorResult;
                }
                admin.Email = requestModel.Email;
            }
            if (!string.IsNullOrEmpty(requestModel.PhoneNo))
            {
                if (requestModel.PhoneNo.Length < 9)
                {
                    errorMsg = "Phone number must contain at least 9 numbers!";
                    goto ReturnErrorResult;
                } 
                admin.Phoneno = requestModel.PhoneNo;
                
            }

            if (!string.IsNullOrEmpty(requestModel.Password))
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
                admin.Modifiedby = requestModel.Admin;
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

        ReturnResult:
        return responseModel;

        ReturnErrorResult:
        return Result<AdminResponseModel>.ValidationError(errorMsg);
    }
    #endregion

    #region DeleteAdmin
    public async Task<Result<AdminResponseModel>> DeleteAdminByCode(string code)
    {
        var responseModel = new Result<AdminResponseModel>();
        var model = new AdminResponseModel();

        if (string.IsNullOrEmpty(code))
        {
            responseModel = Result<AdminResponseModel>.UserInputError("The admin code cannot be null or empty.");
            return responseModel;
        }

        var data = await _db.TblAdmins
                        .AsNoTracking()
                        .Where(x=>x.Deleteflag==false)
                        .FirstOrDefaultAsync(x=>x.Admincode == code);

        if (data is null)
        {
            responseModel = Result<AdminResponseModel>.NotFoundError("There is no admin with this admin code.");
            return responseModel;
        }

        try
        {
            data.Deleteflag = true;
            _db.Entry(data).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            responseModel = Result<AdminResponseModel>.Success("Admin data is deleted successfully.");
        } catch (Exception ex)
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
        var admin = _db.TblAdmins.AsNoTracking().FirstOrDefault(x=>x.Email == email);
        if (admin is null) return false;
        return true;
    }

    private int getLastIndex()
    {
        var adminCode = _db.TblAdmins
                            .AsNoTracking()
                            .OrderByDescending(x => x.Createdat)
                            .Select(x=>x.Admincode)
                            .FirstOrDefault();
        
        if (adminCode is null) return 1;

        var lastIndex = adminCode.Substring(2);
        return int.Parse(lastIndex)+1;
    }

    private string generateAdminCode()
    {
        string adminCode = "A" + getLastIndex().ToString("D4");
        return adminCode;
    }
    #endregion  
}
