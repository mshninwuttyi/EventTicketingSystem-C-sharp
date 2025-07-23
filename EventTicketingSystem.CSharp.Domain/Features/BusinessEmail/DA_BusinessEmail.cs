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

    public async Task<Result<BusinessEmailResponseModel>> Create(BusinessEmailRequestModel requestModel)
    {
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