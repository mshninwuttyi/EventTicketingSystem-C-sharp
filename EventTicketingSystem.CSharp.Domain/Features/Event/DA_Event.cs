namespace EventTicketingSystem.CSharp.Domain.Features.Event;

public class DA_Event : AuthorizationService
{
    private readonly ILogger<DA_Event> _logger;
    private readonly AppDbContext _db;
    private readonly CommonService _commonService;

    public DA_Event(IHttpContextAccessor httpContextAccessor,
                    ILogger<DA_Event> logger,
                    AppDbContext db,
                    CommonService commonService) : base(httpContextAccessor)
    {
        _logger = logger;
        _db = db;
        _commonService = commonService;
    }

    #region Event List

    public async Task<Result<EventListResponseModel>> List()
    {
        var responseModel = new Result<EventListResponseModel>();
        var model = new EventListResponseModel();

        try
        {
            var data = await _db.TblEvents
                        .Where(x => x.Deleteflag == false)
                        .OrderByDescending(x => x.Eventid)
                        .ToListAsync();
            if (data is null)
            {
                return Result<EventListResponseModel>.NotFoundError("No Data Found.");
            }

            model.EventList = data.Select(EventListModel.FromTblEvent).ToList();
            return Result<EventListResponseModel>.Success(model);
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            return Result<EventListResponseModel>.SystemError(ex.Message);
        }
    }

    #endregion

    #region Event Edit

    public async Task<Result<EventEditResponseModel>> Edit(string eventCode)
    {
        var model = new EventEditResponseModel();

        if (eventCode.IsNullOrEmpty())
        {
            return Result<EventEditResponseModel>.UserInputError("Event Code Required.");
        }

        try
        {
            var item = await _db.TblEvents
                        .FirstOrDefaultAsync(
                            x => x.Eventcode == eventCode &&
                            x.Deleteflag == false
                        );
            if (item is null)
            {
                return Result<EventEditResponseModel>.NotFoundError("Event Not Found.");
            }

            model.Event = EventEditModel.FromTblEvent(item);
            return Result<EventEditResponseModel>.Success(model);
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            return Result<EventEditResponseModel>.SystemError(ex.Message);
        }
    }

    #endregion

    #region Event Create

    public async Task<Result<EventCreateResponseModel>> Create(EventCreateRequestModel requestModel)
    {
        if (requestModel.Eventname.IsNullOrEmpty())
        {
            return Result<EventCreateResponseModel>.ValidationError("Event Name Is Required!");
        }
        if (requestModel.Description.IsNullOrEmpty())
        {
            return Result<EventCreateResponseModel>.ValidationError("Descripiton Is Required!");
        }
        if (requestModel.Address.IsNullOrEmpty())
        {
            return Result<EventCreateResponseModel>.ValidationError("Address Is Required!");
        }
        if (requestModel.Startdate.IsNullOrEmpty())
        {
            return Result<EventCreateResponseModel>.ValidationError("Start Date Is Required!");
        }
        if (requestModel.Enddate.IsNullOrEmpty())
        {
            return Result<EventCreateResponseModel>.ValidationError("End Date Is Required!");
        }
        if (requestModel.Eventstatus.IsNullOrEmpty())
        {
            return Result<EventCreateResponseModel>.ValidationError("Event Status Is Required");
        }
        if (requestModel.Totalticketquantity.IsNullOrEmpty() || requestModel.Totalticketquantity <= 0)
        {
            return Result<EventCreateResponseModel>.ValidationError("Invalid Ticket Quantity!");
        }

        try
        {
            var newEvent = new TblEvent()
            {
                Eventid = GenerateUlid(),
                Eventcode = await _commonService.GenerateSequenceCode(EnumTableUniqueName.Tbl_Event),
                Eventname = requestModel.Eventname,
                Startdate = requestModel.Startdate,
                Enddate = requestModel.Enddate,
                Isactive = true,
                Eventstatus = EnumEventStatus.Upcoming.ToString(),
                Totalticketquantity = requestModel.Totalticketquantity,
                Soldoutcount = 0,
                Createdby = CurrentUserId,
                Createdat = DateTime.Now,
                Deleteflag = false
            };
            await _db.AddAsync(newEvent);
            await _db.SaveAndDetachAsync();

            return Result<EventCreateResponseModel>.Success("Event is successfully Created");
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            return Result<EventCreateResponseModel>.SystemError(ex.Message);
        }
    }

    #endregion

    #region Event Update

    public async Task<Result<EventUpdateResponseModel>> Update(EventUpdateRequestModel requestModel)
    {
        try
        {
            var item = await _db.TblEvents
                        .FirstOrDefaultAsync(
                            x => x.Eventcode == requestModel.EventCode &&
                            x.Deleteflag == false
                        );
            if (item is null)
            {
                return Result<EventUpdateResponseModel>.NotFoundError("Event Not Found.");
            }

            item.Eventname = requestModel.EventName;
            item.Startdate = requestModel.Startdate;
            item.Enddate = requestModel.Enddate;
            item.Isactive = requestModel.Isactive;
            item.Eventstatus = requestModel.Eventstatus;
            item.Totalticketquantity = requestModel.Totalticketquantity;
            item.Modifiedby = CurrentUserId;
            item.Modifiedat = DateTime.Now;
            _db.Entry(item).State = EntityState.Modified;
            await _db.SaveAndDetachAsync();

            return Result<EventUpdateResponseModel>.Success("Event Updated Successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            return Result<EventUpdateResponseModel>.SystemError(ex.Message);
        }
    }

    #endregion

    #region Event Delete

    public async Task<Result<EventDeleteResponseModel>> Delete(string eventCode)
    {
        if (eventCode.IsNullOrEmpty())
        {
            return Result<EventDeleteResponseModel>.ValidationError("Event Code Required.");
        }

        try
        {
            var item = await _db.TblEvents
                        .FirstOrDefaultAsync(
                            x => x.Eventcode == eventCode &&
                            x.Deleteflag == false
                        );
            if (item is null)
            {
                return Result<EventDeleteResponseModel>.NotFoundError("Event Not Found.");
            }

            item.Deleteflag = true;
            item.Modifiedby = CurrentUserId;
            item.Modifiedat = DateTime.Now;
            _db.Entry(item).State = EntityState.Modified;
            await _db.SaveAndDetachAsync();

            return Result<EventDeleteResponseModel>.Success("Event Deleted Successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            return Result<EventDeleteResponseModel>.SystemError(ex.Message);
        }
    }

    #endregion
}