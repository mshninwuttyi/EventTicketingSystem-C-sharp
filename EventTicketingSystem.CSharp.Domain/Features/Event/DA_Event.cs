using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventTicketingSystem.CSharp.Domain.Models.Features.Event;

namespace EventTicketingSystem.CSharp.Domain.Features.Event
{
    public class DA_Event
    {
        private readonly ILogger<DA_Event> _logger;
        private readonly AppDbContext _db;
        public DA_Event(ILogger<DA_Event> logger, AppDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        #region ReadAll
        public async Task<Result<EventListResponseModel>> GetEventListAsync()
        {
            var responseModel = new Result<EventListResponseModel>();
            var model = new EventListResponseModel();
            try
            {
                var data = await _db.TblEvents
                    .Where( x => x.Deleteflag == false )
                    .ToListAsync();
                model.EventListResponse = data.Select( x=> new EventResponseModel
                {
                    Eventname = x.Eventname,
                    Description = x.Description,
                    Address = x.Address,
                    Startdate = x.Startdate,
                    Enddate = x.Enddate,
                    Eventimage = x.Eventimage,
                    Eventstatus = x.Eventstatus,

                }).ToList();

                return Result<EventListResponseModel>.Success(model);

            }
            catch (Exception ex)
            {
                _logger.LogExceptionError(ex);

                return Result<EventListResponseModel>.SystemError(ex.Message);
                throw;
            }
        }
        #endregion
        #region ReadDtail
        public async Task<Result<EventResponseModel>> EventDetial(string? EventCode)
        {
            var model = new EventResponseModel();

            if(EventCode == null)
            {
                return Result<EventResponseModel>.UserInputError("Need Event Code!");
            }

            try
            {
                var item = await _db.TblEvents.FirstOrDefaultAsync(x => x.Eventcode == EventCode 
                && x.Deleteflag == false);

                if( item == null)
                {
                    return Result<EventResponseModel>.NotFoundError("The event not found!");
                }

                model.Eventname = item.Eventname;
                model.Description = item.Description;
                model.Address = item.Address;
                model.Startdate  = item.Startdate;
                model.Enddate = item.Enddate;
                model.Eventimage = item.Eventimage;
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
        #region CreateEvent
        public Result<EventResponseModel> CreateEvent(EventRequestModel requestModel)
        {
            var responseModel = new Result<EventResponseModel>();
            var model = new EventResponseModel();

            if(requestModel.Eventname.IsNullOrEmpty())
            {
                return  Result<EventResponseModel>.ValidationError("Event Name Is Required!", model);
            }
            if(requestModel.Description.IsNullOrEmpty())
            {
                return  Result<EventResponseModel>.ValidationError("Descripiton Is Required!", model);
            }
            if(requestModel.Address.IsNullOrEmpty())
            {
                return  Result<EventResponseModel>.ValidationError("Address Is Required!", model);
            }
            if(requestModel.Startdate.IsNullOrEmpty())
            {
                return  Result<EventResponseModel>.ValidationError("Start Date Is Required!", model);
            }
            if(requestModel.Enddate.IsNullOrEmpty())
            {
                return  Result<EventResponseModel>.ValidationError("End Date Is Required!", model);
            }
            if(requestModel.Eventimage.IsNullOrEmpty())
            {
                return  Result<EventResponseModel>.ValidationError("Event Image Is Required!", model);
            }
            if(requestModel.Eventstatus.IsNullOrEmpty())
            {
                return  Result<EventResponseModel>.ValidationError("Event Status Is Required", model);
            }
            if(requestModel.Totalticketquantity.IsNullOrEmpty() || requestModel.Totalticketquantity <= 0)
            {
                return   Result<EventResponseModel>.ValidationError("Invalid Ticket Quantity!");
            }

            try
            {
                var newEvent = new TblEvent()
                {
                    Eventid   = Ulid.NewUlid().ToString(),
                    Eventcode = GenerateEventCode(),
                    Eventname = requestModel.Eventname,
                    //Categorycode
                    Description = requestModel.Description,
                    Address   = requestModel.Address,
                    Startdate = requestModel.Startdate,
                    Enddate   = requestModel.Enddate,
                    Eventimage = requestModel.Eventimage,
                    Isactive  = true,
                    Eventstatus = requestModel.Eventstatus,
                    Totalticketquantity = requestModel.Totalticketquantity,
                    //Businessownercode
                    Soldoutcount = 0,
                    Createdby= requestModel.Admin !,
                    Createdat = DateTime.Now,
                    Deleteflag = false
                };
                _db.AddAsync(newEvent);
                _db.SaveChangesAsync();

                model = new EventResponseModel
                {
                    Eventname = newEvent.Eventname,
                    Description = newEvent.Description,
                    Address = newEvent.Address,
                    Startdate = newEvent.Startdate,
                    Enddate = newEvent.Enddate,
                    Eventimage = newEvent.Eventimage,
                    Isactive = true,
                    Eventstatus = newEvent.Eventstatus,
                    Totalticketquantity = newEvent.Totalticketquantity,

                };
                return  Result<EventResponseModel>.Success(model , "Event is successfully Created");
            }
            catch (Exception ex)
            {
                _logger.LogExceptionError(ex);
                return  Result<EventResponseModel>.SystemError(ex.Message);
            }

        }
        #endregion
        #region UpdateEvent
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
                var item =await _db.TblEvents.FirstOrDefaultAsync(x => x.Eventcode == requestModel.Eventcode);

                if (item == null)
                {
                    responseModel = Result<EventResponseModel>.NotFoundError("The Event doesn't Exist!");
                        goto Result;
                }

                item.Eventname = requestModel.Eventname;
                item.Description = requestModel.Description;
                item.Address = requestModel.Address;
                item.Startdate = requestModel.Startdate;
                item.Enddate = requestModel.Enddate;
                item.Eventimage = requestModel.Eventimage;
                item.Isactive = requestModel.Isactive;
                item.Eventstatus = requestModel.Eventstatus;
                item.Totalticketquantity = requestModel.Totalticketquantity;
                item.Modifiedby = requestModel.Admin;
                item.Modifiedat = DateTime.Now;  

                _db.Update(item);
                _db.SaveChanges();  

                model.Eventname = item.Eventname;
                model.Description = item.Description;   
                model.Address = item.Address;
                model.Startdate = item.Startdate;
                model.Enddate = item.Enddate;
                model.Eventimage = item.Eventimage;
                model.Isactive = item.Isactive;
                model.Eventstatus = item.Eventstatus;
                model.Totalticketquantity = item.Totalticketquantity;

                responseModel = Result<EventResponseModel>.Success(model);
            Result:
                return responseModel;

            }
            catch (Exception ex)
            {
                _logger.LogExceptionError(ex);
                responseModel = Result<EventResponseModel>.SystemError(ex.Message);
                return responseModel;
            }
        }
        #endregion
        #region DeleteEvent
        public async Task<Result<EventResponseModel>> DeleteEvent(string EventCode , string Admin)
        {
            var responseModel = new EventResponseModel();
            if ( EventCode.IsNullOrEmpty())
            {
                return Result<EventResponseModel>.ValidationError("Need Event Code!");
            }
            if( Admin.IsNullOrEmpty() )
            {
                return Result<EventResponseModel>.ValidationError("Admin Need!");
            }

            try
            {

            var item = await _db.TblEvents.FirstOrDefaultAsync(x => x.Eventcode == EventCode && x.Deleteflag == false);

            if( item == null )
                {
                    return Result<EventResponseModel>.NotFoundError("Event Not Found!");
                }

            item.Deleteflag = true;
            item.Modifiedby = Admin;
            item.Modifiedat = DateTime.Now;

                await _db.SaveChangesAsync();

                responseModel = new EventResponseModel
                {
                    Eventname = item.Eventname,
                    Description = item.Description,
                    Startdate = item.Startdate,
                    Enddate = item.Enddate,
                    Address = item.Address,
                    Eventimage = item.Eventimage,
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

        #region Functions
        internal string GenerateEventCode()
        {
            var eventCount = _db.TblEvents.Count();
            return "EV" + eventCount.ToString("D6");
        }
        #endregion
    }

}
