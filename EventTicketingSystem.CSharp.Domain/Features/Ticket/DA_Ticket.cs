using EventTicketingSystem.CSharp.Domain.Models.Features.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTicketingSystem.CSharp.Domain.Features.Ticket
{
    public class DA_Ticket
    {
        private readonly ILogger<BL_Ticket> _logger;
        private readonly AppDbContext _db;
        public DA_Ticket(ILogger<BL_Ticket> logger, AppDbContext db)
        {
            _logger = logger;
            _db = db;
        }
        public async Task<Result<List<TicketResponseModel>>> GetTicketList()
        {
            try
            {
                var tickets = await _db.TblTickets
                                .AsNoTracking()
                                .Include(t => t.TicketPrice)
                                .ThenInclude(p => p.TicketType)
                                .Select(t => new TicketResponseModel
                                    {
                                        Ticketid = t.Ticketid,
                                        Ticketcode = t.Ticketcode,
                                        Ticketpricecode = t.Ticketpricecode,
                                        Isused = t.Isused,
                                        Createdby = t.Createdby,
                                        Createdat = t.Createdat,
                                        Modifiedby = t.Modifiedby,
                                        Modifiedat = t.Modifiedat,
                                        Deleteflag = t.Deleteflag,
                                        Ticketpriceid = t.TicketPrice!.Ticketpriceid,
                                        Eventcode = t.TicketPrice.Eventcode,
                                        Tickettypecode = t.TicketPrice.Tickettypecode,
                                        Ticketprice = t.TicketPrice.Ticketprice,
                                        Ticketquantity = t.TicketPrice.Ticketquantity,
                                        Tickettypeid = t.TicketPrice.TicketType!.Tickettypeid,
                                        Tickettypename = t.TicketPrice.TicketType.Tickettypename
                                    })
                                    .ToListAsync();

                return Result<List<TicketResponseModel>>.Success(tickets);
            }
            catch (Exception ex)
            {
                _logger.LogExceptionError(ex);
                return Result<List<TicketResponseModel>>.SystemError(ex.Message);
            }
        }
    }
}
