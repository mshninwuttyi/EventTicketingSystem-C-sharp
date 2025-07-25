﻿namespace EventTicketingSystem.CSharp.Domain.Features.VerificationCode;

public class DA_VerificationCode
{
    private readonly ILogger<DA_VerificationCode> _logger;
    private readonly AppDbContext _db;
    private readonly EmailService _emailService;
    private const string CreatedByUserId = "Admin";

    public DA_VerificationCode(ILogger<DA_VerificationCode> logger, AppDbContext db, EmailService emailService)
    {
        _logger = logger;
        _db = db;
        _emailService = emailService;
    }

    #region Private Functions

    //private string GenerateRandomVerificationCode()
    //{
    //    byte[] bytes = new byte[4];
    //    using (var rng = RandomNumberGenerator.Create())
    //    {
    //        rng.GetBytes(bytes);
    //    }

    //    int value = Math.Abs(BitConverter.ToInt32(bytes, 0)) % 1000000;
    //    return value.ToString("D6");
    //}

    #endregion

    #region Get Verification Code List

    public async Task<Result<VCResponseModel>> GetList()
    {
        var responseModel = new Result<VCResponseModel>();
        var model = new VCResponseModel();
        try
        {
            var data = await _db.TblVerifications
                        .Where(x => x.Deleteflag == false)
                        .ToListAsync();
            if (data is null)
            {
                return Result<VCResponseModel>.NotFoundError("No Verification Found.");
            }

            model.VerificationCodes = data.Select(x => new VCodeModel
            {
                Verificationid = x.Verificationid,
                Verificationcode = x.Verificationcode,
                Email = x.Email,
                Createdat = x.Createdat,
                Createdby = x.Createdby,
                Modifiedat = x.Modifiedat,
                Modifiedby = x.Modifiedby,
                Deleteflag = false
            }).ToList();

            return Result<VCResponseModel>.Success(model);
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            return Result<VCResponseModel>.SystemError(ex.Message);
        }
    }

    #endregion

    #region Get Verification Code

    public async Task<Result<VCResponseModel>> GetVCodeById(string? id)
    {
        var responseModel = new Result<VCResponseModel>();
        var model = new VCResponseModel();

        if (id is null)
        {
            return Result<VCResponseModel>.ValidationError("Id can't be null here.");
        }

        try
        {
            var data = await _db.TblVerifications
                        .FirstOrDefaultAsync(
                            x => x.Verificationid == id &&
                            x.Deleteflag == false
                        );

            if (data is null)
            {
                return Result<VCResponseModel>.NotFoundError($"Verification Code with Id: {id} is not found!");
            }

            model.VerificationCode = VCodeModel.FromTblVerification(data);
            return Result<VCResponseModel>.Success(model, "Here is the verification code!");
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            return Result<VCResponseModel>.SystemError(ex.Message);
        }
    }

    #endregion

    #region Verify Code
    public async Task<Result<bool>> VerifyCodeByEmail(string? email, string? code)
    {
        var responseModel = new Result<bool>();
        try
        {
            if (email.IsNullOrEmpty() || code.IsNullOrEmpty())
            {
                responseModel = Result<bool>.ValidationError("Invalid Email or Code!");
                goto ReturnResult;
            }

            var data = await _db.TblVerifications
                        .AsNoTracking()
                        .Where(x => x.Deleteflag == false && x.Email == email).OrderByDescending(x => x.Createdat).FirstOrDefaultAsync();

            if (data != null)
            {
                if (DateTime.Now > data.Expiredtime)
                {
                    responseModel = Result<bool>.ValidationError("Verification code expired!");
                    goto ReturnResult;
                }
                if (data.Verificationcode.Equals(code))
                {
                    responseModel = Result<bool>.Success(true, "The verification code is correct!");
                }
                else
                {
                    responseModel = Result<bool>.Success(false, "The verification code is incorrect!");
                }
            }
            else
            {
                responseModel = Result<bool>.NotFoundError($"There is no verification code for {email}!");
            }
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            responseModel = Result<bool>.SystemError(ex.Message);
        }
    ReturnResult:
        return responseModel;
    }
    #endregion

    #region Create Verification Code

    public async Task<Result<VCResponseModel>> CreateVCode(VCRequestModel reqData)
    {
        if (reqData.Email.IsNullOrEmpty() || !reqData.Email!.IsValidEmail())
        {
            return Result<VCResponseModel>.ValidationError("Email is invalid!");
        }

        try
        {
            string otp = new Random().Next(100000, 999999).ToString();
            var expiry = DateTime.Now.AddMinutes(3);
            var newVC = new TblVerification()
            {
                Verificationid = GenerateUlid(),
                Verificationcode = otp,
                Email = reqData.Email!,
                Expiredtime = expiry,
                Createdby = CreatedByUserId,
                Createdat = DateTime.Now,
                Deleteflag = false
            };

            await _db.TblVerifications.AddAsync(newVC);
            await _db.SaveAndDetachAsync();

            var emailTemplate = new EmailModel()
            {
                Email = reqData.Email!,
                Subject = EmailSubjectTemplates.Verification,
                Body = EmailBodyTemplates.Otp.Replace("(@otp)", otp)
            };

            var result = await _emailService.SendEmailVerification(emailTemplate);
            if (result is false)
            {
                return Result<VCResponseModel>.SystemError("Failed to send verification code via email.");
            }

            return Result<VCResponseModel>.Success("Verification Code send to your Email.");    //$"Here is the verification code for {reqData.Email}
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            return Result<VCResponseModel>.SystemError(ex.Message);
        }
    }

    #endregion
}