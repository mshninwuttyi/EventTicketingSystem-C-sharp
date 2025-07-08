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

    public async Task<Result<BusinessOwnerResponseModel>> GetList(BusinessOwnerRequestModel requestModel)
    {
        var responseModel = new Result<BusinessOwnerResponseModel>();
        var model = new BusinessOwnerResponseModel();
        try
        {
            var data = await _db.TblBusinessowners
                        .AsNoTracking()
                        .ToListAsync();

            model.BusinessOwners = data.Select(x => new BusinessOwnerModel
            {
                BusinessOwnerId = x.Businessownerid,
                BusinessOwnerCode = x.Businessownercode,
                Name = x.Name,
                Email = x.Email,
                PhoneNumber = x.Phonenumber,
            }).ToList();

            return Result<BusinessOwnerResponseModel>.Success(model);
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            return Result<BusinessOwnerResponseModel>.Error(ex.Message);
        }
    }
}