namespace EventTicketingSystem.CSharp.Domain.Features.BusinessEmail;

public class DA_BusinessEmail
{
    private readonly ILogger<DA_BusinessEmail> _logger;
    private readonly AppDbContext _db;
    private readonly CommonService _commonService;
    private const string CreatedByUserId = "User";

#warning We will only use EnumTableUniqueName for generating sequence codes.
    private const string BusinessEmailCodePrefix = "BE";

#warning We will no longer use this method to generate business email codes, as we will use a sequence code generator instead.
    private string NextBusinessEmailCode()
    {
        var count = _db.TblBusinessemails.Count() + 1; // Count existing emails and add 1
        return $"{BusinessEmailCodePrefix}{count:D6}"; // Format with leading zeros
    }

    public DA_BusinessEmail(ILogger<DA_BusinessEmail> logger, AppDbContext db, CommonService commonService)
    {
        _logger = logger;
        _db = db;
        _commonService = commonService;
    }

    public async Task<Result<BusinessEmailResponseModel>> Create(BusinessEmailRequestModel requestModel)
    {
        try
        {
#warning We will use GenerateUlid() to generate unique IDs from DevCode
            // Create a new business email and save to database
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

#warning We can use newBusinessEmail instead of entry.Entity after successful saving to the database

            // return the created business email response model
            var createdBusinessEmail = entry.Entity;
            var responseModel = new BusinessEmailResponseModel
            {
                BusinessEmailId = createdBusinessEmail.Businessemailid,
                BusinessEmailCode = createdBusinessEmail.Businessemailcode,
                FullName = createdBusinessEmail.Fullname,
                Phone = createdBusinessEmail.Phone,
                Email = createdBusinessEmail.Email
            };
            return Result<BusinessEmailResponseModel>.Success(responseModel, "Business Email created successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            return Result<BusinessEmailResponseModel>.SystemError(ex.Message);
        }
    }

    public async Task<Result<BusinessEmailResponseModel>> GetById(string id)
    {
        try
        {
            var data = await _db.TblBusinessemails.FirstOrDefaultAsync(b => b.Businessemailid == id);

#warning We can use 'is null' keyword instead of '== null' for null checks in C# 7.0 and later
            if (data is null)
            {
                return Result<BusinessEmailResponseModel>.NotFoundError("No Data Found!");
            }

            var model = new BusinessEmailResponseModel
            {
                BusinessEmailId = data.Businessemailid,
                BusinessEmailCode = data.Businessemailcode,
                FullName = data.Fullname,
                Phone = data.Phone,
                Email = data.Email
            };

            return Result<BusinessEmailResponseModel>.Success(model);
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            return Result<BusinessEmailResponseModel>.SystemError(ex.Message);
        }
    }

    public async Task<Result<BusinessEmailListResponseModel>> GetList()
    {
        var model = new BusinessEmailListResponseModel();
        try
        {
#warning We need to add some conditions to check if the id is valid e.g. deleteflag is false, etc.
            var data = await _db.TblBusinessemails.Where(x => x.Deleteflag == false).ToListAsync();

            model.BusinessEmails = data.Select(x => new BusinessEmailModel
            {
                BusinessEmailId = x.Businessemailid,
                BusinessEmailCode = x.Businessemailcode,
                FullName = x.Fullname,
                Phone = x.Phone,
                Email = x.Email
            }).ToList();

            return Result<BusinessEmailListResponseModel>.Success(model);
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            return Result<BusinessEmailListResponseModel>.SystemError(ex.Message);
        }
    }
}