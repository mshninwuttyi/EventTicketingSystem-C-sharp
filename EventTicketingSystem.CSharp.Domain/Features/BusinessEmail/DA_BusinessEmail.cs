using EventTicketingSystem.CSharp.Domain.Models.Features.BusinessEmail;

namespace EventTicketingSystem.CSharp.Domain.Features.BusinessEmail;

public class DA_BusinessEmail
{
    private readonly ILogger<DA_BusinessEmail> _logger;
    private readonly AppDbContext _db;

    private const string BusinessEmailCodePrefix = "BE";
    private const string CreatedByUserId = "User";

    private string NextBusinessEmailCode()
    {
        var count = _db.TblBusinessemails.Count() + 1; // Count existing emails and add 1
        return $"{BusinessEmailCodePrefix}{count:D6}"; // Format with leading zeros
    }

    public DA_BusinessEmail(ILogger<DA_BusinessEmail> logger, AppDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    public async Task<Result<BusinessEmailResponseModel>> Create(BusinessEmailRequestModel requestModel)
    {
        try
        {

            // Create a new business email and save to database
            var newBusinessEmail = new TblBusinessemail
            {
                Businessemailid = Ulid.NewUlid().ToString(),
                Businessemailcode = NextBusinessEmailCode(),
                Fullname = requestModel.FullName,
                Phone = requestModel.Phone,
                Email = requestModel.Email,
                Createdby = CreatedByUserId,
                Createdat = DateTime.Now,
                Deleteflag = false
            };
            var entry = await _db.TblBusinessemails.AddAsync(newBusinessEmail);
            await _db.SaveChangesAsync();

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

            if (data == null)
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
            var data = await _db.TblBusinessemails.ToListAsync();

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
