namespace EventTicketingSystem.CSharp.Domain.Features.TicketType;

public class DA_TicketType
{
    private readonly Logger<DA_TicketType> _logger;
    private readonly AppDbContext _db;
    private readonly CommonService _commonService;
    private const string CreatedByUserId = "Admin";

    public DA_TicketType(Logger<DA_TicketType> logger, AppDbContext db, CommonService commonService)
    {
        _logger = logger;
        _db = db;
        _commonService = commonService;
    }

    #region List
    public async Task<Result<TicketTypeListResponseModel>> ListAsync()
    {
            var resopnseModel = new Result<TicketTypeListResponseModel>();
            var model = new TicketTypeListResponseModel();

            try
            {
                var data =  await _db.TblTickettypes.
                    Where(x => x.Deleteflag == false)
                    .OrderByDescending(x => x.Tickettypeid)
                    .ToListAsync();
                if (data is null)
                {
                    return Result<TicketTypeListResponseModel>.NotFoundError("Data not Found.");
                }

                model.TicketTypeList = data.Select(TicketTypeListModel.FromTblTickettype).ToList();

                return Result<TicketTypeListResponseModel>.Success(model);

            }
            catch (Exception ex)
            {
                _logger.LogExceptionError(ex);
                return Result<TicketTypeListResponseModel>.SystemError(ex.Message);
            }
    }
    #endregion
    #region Edit
    public async Task<Result<TicketTypeEditResponseModel>> Edit(string ticketTypeCode)
    {
        var model = new TicketTypeEditResponseModel();

        if(ticketTypeCode is null)
        {
            return Result<TicketTypeEditResponseModel>.UserInputError("Ticket Type Code is required.");
        }

        try
        {
            var item = await _db.TblTickettypes
                .FirstOrDefaultAsync(x => x.Tickettypecode == ticketTypeCode
                && x.Deleteflag == false);

            if (item is null)
            {
                return Result<TicketTypeEditResponseModel>.NotFoundError("Ticket Type Not Found.");
            }

            model.ticketTypeEditModel = TicketTypeEditModel.FromTblTickettypes(item);
            return Result<TicketTypeEditResponseModel>.Success( model);
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            return Result<TicketTypeEditResponseModel>.SystemError(ex.Message);

        }
    }
    #endregion
    #region Create
    public async Task<Result<TicketTypeCreateResponseModel>> Create(TicketTypeCreateRequestModel requestModel)
    {
        if(requestModel.Tickettypename.IsNullOrEmpty())
        {
            return Result<TicketTypeCreateResponseModel>.ValidationError("Ticket Type Name is Required.");
        }

        try
        {
            var NewTicketType = new TblTickettype
            {
                Tickettypeid = GenerateUlid(),
                Tickettypecode = await _commonService.GenerateSequenceCode(EnumTableUniqueName.Tbl_TicketType),
                Tickettypename = requestModel.Tickettypename!,
                Createdby = CreatedByUserId,
                Createdat = DateTime.Now,
                Deleteflag = false
            };

            await _db.AddAsync(NewTicketType);
            await _db.SaveChangesAsync();

            return Result<TicketTypeCreateResponseModel>.Success("Ticket Type Create Successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            return Result<TicketTypeCreateResponseModel>.SystemError(ex.Message);
        }
    }
    #endregion

    #region Update
    public async Task<Result<TicketTypeUpdateResponseModel>> Update(TicketTypeUpdateRequestModel requestModel)
    {
        if (requestModel.Tickettypecode.IsNullOrEmpty())
        {
            return Result<TicketTypeUpdateResponseModel>.ValidationError("Need Ticket Type Code");
        }
        if (requestModel.Tickettypename.IsNullOrEmpty())
        {
            return Result<TicketTypeUpdateResponseModel>.ValidationError("Need Ticket Type Name.");
        }

        try
        {
            var item = await _db.TblTickettypes
                .FirstOrDefaultAsync(x => x.Tickettypecode == requestModel.Tickettypecode &&
                x.Deleteflag == false);

            if (item == null)
            {
                return Result<TicketTypeUpdateResponseModel>.NotFoundError("Ticket Type Not found.");
            }

            item.Tickettypename = requestModel.Tickettypename;

            await _db.SaveChangesAsync();

            return Result<TicketTypeUpdateResponseModel>.Success("Ticket Type Update Success.");
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            return Result<TicketTypeUpdateResponseModel>.SystemError(ex.Message);
        }
    }
    #endregion
    #region Delete
    public async Task<Result<TicketTypeDeleteResponseModel>> Delete(string ticketTypeCode)
    {
        if(ticketTypeCode == null)
        {
            return Result<TicketTypeDeleteResponseModel>.ValidationError("Need Ticket Type Code.");
        }

        try
        {
            var item = await _db.TblTickettypes
                .FirstOrDefaultAsync( x => x.Tickettypecode == ticketTypeCode &&
                x.Deleteflag == false);

            if (item is null)
            {
                return Result<TicketTypeDeleteResponseModel>.NotFoundError("Ticket Type not Found.");
            }

            item.Deleteflag = true;
            item.Modifiedby = CreatedByUserId;
            item.Modifiedat = DateTime.Now;

            await _db.SaveChangesAsync();

            return Result<TicketTypeDeleteResponseModel>.Success("Ticke Type Dlete Success.");
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            return Result<TicketTypeDeleteResponseModel>.SystemError(ex.Message);
        }
    }
    #endregion
}