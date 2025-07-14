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

        public async Task<Result<EventListResponseModel>> GetEventList()
        {
            var responseModel = new Result<EventListResponseModel>();
            var model = new EventListResponseModel();
            try
            {
                var data = await _db.TblEvents.ToListAsync();
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

        public Result<EventResponseModel> CreateEvent(EventRequestModel requestModel)
        {
            var model = new EventResponseModel();

            if(requestModel.Eventname.IsNullOrEmpty())
            {
                return Result<EventResponseModel>.ValidationError("Event Name Is Required!", model);
            }
            if(requestModel.Description.IsNullOrEmpty())
            {
                return Result<EventResponseModel>.ValidationError("Descripiton Is Required!", model);
            }
            if(requestModel.Address.IsNullOrEmpty())
            {
                return Result<EventResponseModel>.ValidationError("Address Is Required!", model);
            }
            if(requestModel.Startdate.IsNullOrEmpty())
            {
                return Result<EventResponseModel>.ValidationError("Start Date Is Required!", model);
            }
            if(requestModel.Enddate.IsNullOrEmpty())
            {
                return Result<EventResponseModel>.ValidationError("End Date Is Required!", model);
            }
            if(requestModel.Eventimage.IsNullOrEmpty())
            {
                return Result<EventResponseModel>.ValidationError("Event Image Is Required!", model);
            }
            if(requestModel.Eventstatus.IsNullOrEmpty())
            {
                return Result<EventResponseModel>.ValidationError("Event Status Is Required", model);
            }
            if(requestModel.Totalticketquantity.IsNullOrEmpty() || requestModel.Totalticketquantity <= 0)
            {
                return Result<EventResponseModel>.ValidationError("Invalid Ticket Quantity!");
            }

            try
            {
                _db.TblEvents.Add(new TblEvent()
                {
                    Eventid = Ulid.NewUlid().ToString(),
                    //Eventcode = "E00"
                    Eventname = requestModel.Eventname,
                    //Categorycode
                    Description = requestModel.Description,
                    Address = requestModel.Address,
                    Startdate = requestModel.Startdate,
                    Enddate = requestModel.Enddate,
                    Eventimage = requestModel.Eventimage,
                    Isactive = true,
                    Eventstatus = requestModel.Eventstatus,
                    Totalticketquantity = requestModel.Totalticketquantity,
                    //Businessownercode
                    Soldoutcount = 0,
                    Createdby = requestModel.Admin,
                    Createdat = DateTime.Now,
                    Deleteflag = false
                });

                _db.SaveChanges();
                return Result<EventResponseModel>.Success(model);
            }
            catch (Exception ex)
            {
                _logger.LogExceptionError(ex);
                return Result<EventResponseModel>.SystemError(ex.Message);
                throw;
            }
        }

        public Result<EventResponseModel> UpdateEvent(EventRequestModel requestModel)
        {
            var model = new EventResponseModel();
            if (requestModel.Admin.IsNullOrEmpty())
            {
                return Result<EventResponseModel>.ValidationError("Need admin!", model);
            }
            try
            {
                var item = _db.TblEvents.FirstAsync( x=> x.Eventcode == requestModel.)
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        

    }
}
