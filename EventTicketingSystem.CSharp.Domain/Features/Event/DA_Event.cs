namespace EventTicketingSystem.CSharp.Domain.Features.Event;

public class DA_Event
{
    private readonly ILogger<DA_Event> _logger;
    private readonly AppDbContext _db;
    private readonly CommonService _commonService;
    private const string CreatedByUserId = "Admin";

    public DA_Event(ILogger<DA_Event> logger, AppDbContext db, CommonService commonService)
    {
        _logger = logger;
        _db = db;
        _commonService = commonService;
    }

    #region Read All

    public async Task<Result<EventListResponseModel>> GetEventList()
    {
        var responseModel = new Result<EventListResponseModel>();
        var model = new EventListResponseModel();

        try
        {
            var data = await _db.TblEvents
                .Where(x => x.Deleteflag == false)
                .ToListAsync();
            model.EventListResponse = data.Select(x => new EventResponseModel
            {
                Eventname = x.Eventname,
                Startdate = x.Startdate,
                Enddate = x.Enddate,
                Eventstatus = x.Eventstatus,

            }).ToList();

            return Result<EventListResponseModel>.Success(model);

        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            return Result<EventListResponseModel>.SystemError(ex.Message);
        }
    }

    #endregion

    #region Event Detail

    public async Task<Result<EventResponseModel>> EventDetail(string? eventCode)
    {
        var model = new EventResponseModel();

        if (eventCode.IsNullOrEmpty())
        {
            return Result<EventResponseModel>.UserInputError("Need Event Code!");
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
                return Result<EventResponseModel>.NotFoundError("The event not found!");
            }

            model.Eventname = item.Eventname;
            model.Startdate = item.Startdate;
            model.Enddate = item.Enddate;
            model.Isactive = item.Isactive;
            model.Eventstatus = item.Eventstatus;
            model.Totalticketquantity = item.Totalticketquantity;

            return Result<EventResponseModel>.Success(model);
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            return Result<EventResponseModel>.SystemError(ex.Message);
        }
    }

    #endregion

    #region Create Event

    public async Task<Result<EventResponseModel>> CreateEvent(EventRequestModel requestModel)
    {
        var responseModel = new Result<EventResponseModel>();
        var model = new EventResponseModel();

        if (requestModel.Eventname.IsNullOrEmpty())
        {
            return Result<EventResponseModel>.ValidationError("Event Name Is Required!");
        }
        if (requestModel.Description.IsNullOrEmpty())
        {
            return Result<EventResponseModel>.ValidationError("Descripiton Is Required!");
        }
        if (requestModel.Address.IsNullOrEmpty())
        {
            return Result<EventResponseModel>.ValidationError("Address Is Required!");
        }
        if (requestModel.Startdate.IsNullOrEmpty())
        {
            return Result<EventResponseModel>.ValidationError("Start Date Is Required!");
        }
        if (requestModel.Enddate.IsNullOrEmpty())
        {
            return Result<EventResponseModel>.ValidationError("End Date Is Required!");
        }
        if (requestModel.Eventimage.IsNullOrEmpty())
        {
            return Result<EventResponseModel>.ValidationError("Event Image Is Required!");
        }
        if (requestModel.Eventstatus.IsNullOrEmpty())
        {
            return Result<EventResponseModel>.ValidationError("Event Status Is Required");
        }
        if (requestModel.Totalticketquantity.IsNullOrEmpty() || requestModel.Totalticketquantity <= 0)
        {
            return Result<EventResponseModel>.ValidationError("Invalid Ticket Quantity!");
        }

        try
        {
            var newEvent = new TblEvent()
            {
                Eventid = Ulid.NewUlid().ToString(),
                Eventcode = await _commonService.GenerateSequenceCode(EnumTableUniqueName.Tbl_Event),
                Eventname = requestModel.Eventname,
                Startdate = requestModel.Startdate,
                Enddate = requestModel.Enddate,
                Isactive = true,
                Eventstatus = requestModel.Eventstatus,
                Totalticketquantity = requestModel.Totalticketquantity,
                Soldoutcount = 0,
                Createdby = CreatedByUserId,
                Createdat = DateTime.Now,
                Deleteflag = false
            };
            await _db.AddAsync(newEvent);
            await _db.SaveAndDetachAsync();

            model = new EventResponseModel
            {
                Eventname = newEvent.Eventname,
                Startdate = newEvent.Startdate,
                Enddate = newEvent.Enddate,
                Isactive = true,
                Eventstatus = newEvent.Eventstatus,
                Totalticketquantity = newEvent.Totalticketquantity,

            };
            return Result<EventResponseModel>.Success(model, "Event is successfully Created");
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            return Result<EventResponseModel>.SystemError(ex.Message);
        }
    }

    #endregion

    #region Update Event

    public async Task<Result<EventResponseModel>> UpdateEvent(EventRequestModel requestModel)
    {
        var responseModel = new Result<EventResponseModel>();
        var model = new EventResponseModel();

        if (requestModel.Admin.IsNullOrEmpty())
        {
            return Result<EventResponseModel>.ValidationError("Need admin!", model);
        }

        try
        {
            var item = await _db.TblEvents
                        .FirstOrDefaultAsync(
                            x => x.Eventcode == requestModel.Eventcode &&
                            x.Deleteflag == false
                        );
            if (item is null)
            {
                return Result<EventResponseModel>.NotFoundError("The Event doesn't Exist!");
            }

            item.Eventname = requestModel.Eventname;
            item.Startdate = requestModel.Startdate;
            item.Enddate = requestModel.Enddate;
            item.Isactive = requestModel.Isactive;
            item.Eventstatus = requestModel.Eventstatus;
            item.Totalticketquantity = requestModel.Totalticketquantity;
            item.Modifiedby = CreatedByUserId;
            item.Modifiedat = DateTime.Now;

            _db.Entry(item).State = EntityState.Modified;
            await _db.SaveAndDetachAsync();

            model.Eventname = item.Eventname;
            model.Startdate = item.Startdate;
            model.Enddate = item.Enddate;
            model.Isactive = item.Isactive;
            model.Eventstatus = item.Eventstatus;
            model.Totalticketquantity = item.Totalticketquantity;

            responseModel = Result<EventResponseModel>.Success(model);
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            return Result<EventResponseModel>.SystemError(ex.Message);
        }

        return responseModel;
    }

    #endregion

    #region Delete Event

    public async Task<Result<EventResponseModel>> DeleteEvent(string EventCode)
    {
        var responseModel = new EventResponseModel();

        if (EventCode.IsNullOrEmpty())
        {
            return Result<EventResponseModel>.ValidationError("Need Event Code!");
        }

        try
        {
            var item = await _db.TblEvents
                        .FirstOrDefaultAsync(
                            x => x.Eventcode == EventCode &&
                            x.Deleteflag == false
                        );
            if (item is null)
            {
                return Result<EventResponseModel>.NotFoundError("Event Not Found!");
            }

            item.Deleteflag = true;
            item.Modifiedby = CreatedByUserId;
            item.Modifiedat = DateTime.Now;
            _db.Entry(item).State = EntityState.Modified;
            await _db.SaveAndDetachAsync();

            responseModel = new EventResponseModel
            {
                Eventname = item.Eventname,
                Startdate = item.Startdate,
                Enddate = item.Enddate,
                Eventstatus = item.Eventstatus,
                Totalticketquantity = item.Totalticketquantity,
            };

            return Result<EventResponseModel>.Success(responseModel, "Event Delet Successfully!");
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            return Result<EventResponseModel>.SystemError(ex.Message);
        }
    }

    #endregion
}