using System.Security.Cryptography;
namespace EventTicketingSystem.CSharp.Domain.Features.VerificationCode;

public class DA_VerificationCode
{
    private readonly ILogger<DA_VerificationCode> _logger;
    private readonly AppDbContext _db;

    public DA_VerificationCode(ILogger<DA_VerificationCode> logger, AppDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    #region Private Functions
    private string GenerateRandomVerificationCode()
    {
        byte[] bytes = new byte[4];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(bytes);
        }

        int value = Math.Abs(BitConverter.ToInt32(bytes, 0)) % 1000000;
        return value.ToString("D6");
    }
    #endregion

    #region Get Verification Code List
    public async Task<Result<VCResponseModel>> GetList()
    {
        var responseModel = new Result<VCResponseModel>();
        var model = new VCResponseModel();
        try
        {
            var data = await _db.TblVerifications
                        .AsNoTracking()
                        .Where(x => x.Deleteflag == false)
                        .ToListAsync();

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

            responseModel = Result<VCResponseModel>.Success(model, "Here are the list of verification code!");
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            responseModel = Result<VCResponseModel>.SystemError(ex.Message);
        }
        return responseModel;
    }
    #endregion

    #region Get Verification Code
    public async Task<Result<VCResponseModel>> GetVCodeByCode(string code)
    {
        var responseModel = new Result<VCResponseModel>();
        var model = new VCResponseModel();
        try
        {
            var data = await _db.TblVerifications
                        .AsNoTracking()
                        .Where(x => x.Deleteflag == false && x.Verificationcode == code).FirstOrDefaultAsync();

            if (data != null)
            {
                model.VerificationCode = VCodeModel.FromTblVerification(data);
                responseModel = Result<VCResponseModel>.Success(model, "Here is the verification code!");
            }
            else
            {
                responseModel = Result<VCResponseModel>.NotFoundError($"Verification Code with Code: {code} is not found!");
            }
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            responseModel = Result<VCResponseModel>.SystemError(ex.Message);
        }
        return responseModel;
    }
    #endregion

    #region Verify Code
    public async Task<Result<bool>> VerifyCodeByEmail(string email, string code)
    {
        var responseModel = new Result<bool>();
        try
        {
            var data = await _db.TblVerifications
                        .AsNoTracking()
                        .Where(x => x.Deleteflag == false && x.Email == email).OrderByDescending(x => x.Createdat).FirstOrDefaultAsync();

            if (data != null)
            {
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
        return responseModel;
    }
    #endregion

    #region Create Verification Code
    public async Task<Result<VCResponseModel>> CreateVCode(VCRequestModel reqData)
    {
        var responseModel = new Result<VCResponseModel>();
        var model = new VCResponseModel();
        try
        {
            if (reqData.Email.IsNullOrEmpty() || reqData.Email.IsValidEmail())
            {
                responseModel = Result<VCResponseModel>.ValidationError("Email is invalid!");
                goto ReturnResult;
            }
            var newVC = new TblVerification()
            {
                Verificationid = Ulid.NewUlid().ToString(),
                Verificationcode = GenerateRandomVerificationCode(),
                Email = reqData.Email,
                Createdby = "system",
                Createdat = DateTime.Now,
                Deleteflag = false
            };

            _db.TblVerifications.Add(newVC);
            await _db.SaveChangesAsync();

            model.VerificationCode = VCodeModel.FromTblVerification(newVC);
            responseModel = Result<VCResponseModel>.Success(model, $"Here is the verification code for {reqData.Email}");
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            responseModel = Result<VCResponseModel>.SystemError(ex.Message);
        }
    ReturnResult:
        return responseModel;
    }
    #endregion
}
