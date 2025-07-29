namespace EventTicketingSystem.CSharp.Domain.Features.BusinessEmail;

public class DA_BusinessEmail
{
    private readonly ILogger<DA_BusinessEmail> _logger;
    private readonly AppDbContext _db;
    private readonly CommonService _commonService;
    private const string CreatedByUserId = "Admin";

    public DA_BusinessEmail(ILogger<DA_BusinessEmail> logger, AppDbContext db, CommonService commonService)
    {
        _logger = logger;
        _db = db;
        _commonService = commonService;
    }

    public async Task<Result<BusinessEmailCreateResponseModel>> Create(BusinessEmailCreateRequestModel requestModel)
    {
        var responseModel = ValidateRequest(requestModel);
        if (responseModel != null)
        {
            return responseModel;
        }

        try
        {
            var newBusinessEmail = new TblBusinessemail
            {
                Businessemailid = GenerateUlid(),
                Businessemailcode = await _commonService.GenerateSequenceCode(EnumTableUniqueName.Tbl_BusinessEmail),
                Fullname = requestModel.FullName,
                Phone = requestModel.Phone,
                Email = requestModel.Email,
                Createdby = CreatedByUserId,
                Createdat = DateTime.Now,
                Deleteflag = false
            };
            var entry = await _db.TblBusinessemails.AddAsync(newBusinessEmail);
            await _db.SaveAndDetachAsync();

            return Result<BusinessEmailCreateResponseModel>.Success("Business Email created successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            return Result<BusinessEmailCreateResponseModel>.SystemError(ex.Message);
        }
    }

    public async Task<Result<BusinessEmailEditResponseModel>> Edit(string businessEmailCode)
    {
        var model = new BusinessEmailEditResponseModel();

        if (businessEmailCode.IsNullOrEmpty())
        {
            return Result<BusinessEmailEditResponseModel>.ValidationError("Business Email Code is required.");
        }

        try
        {
            var data = await _db.TblBusinessemails
                .FirstOrDefaultAsync(
                x => x.Businessemailcode == businessEmailCode &&
                x.Deleteflag == false);

            if (data is null)
            {
                return Result<BusinessEmailEditResponseModel>.NotFoundError("Business Email Not Found.");
            }

            model.BusinessEmail = BusinessEmailEditModel.FromTblBusinessEmail(data);
            return Result<BusinessEmailEditResponseModel>.Success(model);
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            return Result<BusinessEmailEditResponseModel>.SystemError(ex.Message);
        }
    }

    public async Task<Result<BusinessEmailListResponseModel>> List()
    {
        var responseModel = new Result<BusinessEmailListResponseModel>();
        var model = new BusinessEmailListResponseModel();

        try
        {
            var data = await _db.TblBusinessemails
                .Where(x => x.Deleteflag == false)
                .OrderBy(x => x.Businessemailid)
                .ToListAsync();

            if (data is null)
            {
                responseModel = Result<BusinessEmailListResponseModel>.Success("No Business Emails found.");
            }

            model.BusinessEmailList = data.Select(BusinessEmailListModel.FromTblBusinessEmail).ToList();
            return Result<BusinessEmailListResponseModel>.Success(model);
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            return Result<BusinessEmailListResponseModel>.SystemError(ex.Message);
        }
    }

    private Result<BusinessEmailCreateResponseModel> ValidateRequest(BusinessEmailCreateRequestModel requestModel)
    {
        if (requestModel.FullName.IsNullOrEmpty())
        {
            return Result<BusinessEmailCreateResponseModel>.ValidationError("Full Name cannot be empty.");
        }

        if (requestModel.Phone.IsNullOrEmpty())
        {
            return Result<BusinessEmailCreateResponseModel>.ValidationError("Phone cannot be empty.");
        }

        if (requestModel.Phone.IsNullOrEmpty() || requestModel.Phone.Length < 9)
        {
            return Result<BusinessEmailCreateResponseModel>.ValidationError("Phone number cannot be empty or less than 9 numbers!");
        }

        if (requestModel.Email.IsNullOrEmpty())
        {
            return Result<BusinessEmailCreateResponseModel>.ValidationError("Email cannot be empty.");
        }

        if (!requestModel.Email.IsValidEmail())
        {
            return Result<BusinessEmailCreateResponseModel>.ValidationError("Invalid Email format.");
        }

        if (IsAlreadyUsed(requestModel.Email))
        {
            return Result<BusinessEmailCreateResponseModel>.ValidationError("Email is already in use.");
        }

        return null!;
    }

    private bool IsAlreadyUsed(string email)
    {
        var admin = _db.TblBusinessemails.FirstOrDefault(x => x.Email == email);
        if (admin is null) return false;
        return true;
    }
}