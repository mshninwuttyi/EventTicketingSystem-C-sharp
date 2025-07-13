using EventTicketingSystem.CSharp.Domain.Models.Features.Ticket;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTicketingSystem.CSharp.Domain.Features.Ticket
{
    public class DA_Ticket
    {
        private readonly ILogger<DA_Ticket> _logger;
        private readonly AppDbContext _db;

        public DA_Ticket(ILogger<DA_Ticket> logger, AppDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public string GenerateTicketCode()
        {
            var ticketCount = _db.TblTickets.Count();
            return "T" + ticketCount.ToString("D6");
        }

        public async Task<Result<TicketResponseModel>> CreateTicket(TicketRequestModel requestModel)
        {
            try
            {
                var newTicket = new TblTicket
                {
                    Ticketid = Ulid.NewUlid().ToString(),
                    Ticketcode = GenerateTicketCode(),
                    Ticketpricecode = requestModel.Ticketpricecode,
                    Isused = false,
                    Createdby = "",
                    Createdat = DateTime.Now,
                    Modifiedby = "",
                    Modifiedat = DateTime.Now,
                    Deleteflag = false
                };
                
                await _db.TblTickets.AddAsync(newTicket);
                await _db.SaveChangesAsync();

                var responseModel = new TicketResponseModel
                {
                    Ticketid = newTicket.Ticketid,
                    Ticketcode = newTicket.Ticketcode,
                    Ticketpricecode = newTicket.Ticketpricecode,
                    Isused = newTicket.Isused
                };
                return Result<TicketResponseModel>.Success(responseModel, "Ticket is created successfully!");
            }
            catch (Exception ex)
            {
                _logger.LogExceptionError(ex);
                return Result<TicketResponseModel>.SystemError(ex.Message);
            }
        }
    
        public async Task<Result<TicketListResponseModel>> GetAllTicket()
        {
            var model = new TicketListResponseModel();
            try
            {
                var data = await _db.TblTickets.ToListAsync();

                model.Tickets = data.Select(x => new TicketResponseModel
                {
                    Ticketid = x.Ticketid,
                    Ticketcode = x.Ticketcode,
                    Ticketpricecode = x.Ticketpricecode,
                    Isused = x.Isused
                }).ToList();

                return Result<TicketListResponseModel>.Success(model);
            }
            catch (Exception ex)
            {
                _logger.LogExceptionError(ex);
                return Result<TicketListResponseModel>.SystemError(ex.Message);
            }
        }
    }
}
