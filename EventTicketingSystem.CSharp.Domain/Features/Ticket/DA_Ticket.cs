namespace EventTicketingSystem.CSharp.Domain.Features.Ticket;

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
            await _db.SaveAndDetachAsync();

            var responseModel = new TicketResponseModel
            {
                Ticketid = newTicket.Ticketid,
                Ticketcode = newTicket.Ticketcode,
                Ticketpricecode = newTicket.Ticketpricecode,
                Isused = newTicket.Isused,
                Createdby = newTicket.Createdby,
                Createdat = newTicket.Createdat,
            };
            return Result<TicketResponseModel>.Success(responseModel, "Ticket is created successfully!");
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            return Result<TicketResponseModel>.SystemError(ex.Message);
        }
    }
    public async Task<Result<TicketEditResponseModel>> GetTicketByCode(string ticketCode)
    {
        try
        {
            if (ticketCode.IsNullOrEmpty())
            {
                return Result<TicketEditResponseModel>.ValidationError("Ticket Code cannot be null or empty.");
            }

            var ticket = await _db.TblTickets
                .FirstOrDefaultAsync(x => x.Ticketcode == ticketCode && x.Deleteflag == false);
            if (ticket == null)
            {
                return Result<TicketEditResponseModel>.NotFoundError("No Data Found!");
            }

            var model = new TicketEditResponseModel
            {
                Ticketid = ticket.Ticketid,
                Ticketcode = ticket.Ticketcode,
                Ticketpricecode = ticket.Ticketpricecode,
                Isused = ticket.Isused,
                Createdby = ticket.Createdby,
                Createdat = ticket.Createdat,
                Modifiedby = ticket.Modifiedby,
                Modifiedat = ticket.Modifiedat
            };
            return Result<TicketEditResponseModel>.Success(model);

        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            return Result<TicketEditResponseModel>.SystemError(ex.Message);
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
                Isused = x.Isused,
                Createdby = x.Createdby,
                Createdat = x.Createdat,
                Modifiedby = x.Modifiedby,
                Modifiedat = x.Modifiedat
            }).ToList();

            return Result<TicketListResponseModel>.Success(model);
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            return Result<TicketListResponseModel>.SystemError(ex.Message);
        }
    }

    public async Task<Result<List<TicketResponseModel>>> GetTicketList()
    {
        try
        {
            //var tickets = await _db.TblTickets
            //                .AsNoTracking()
            //                .Include(t => t.Ticketpricecode)
            //                .ThenInclude(p => p.TicketType)
            //                .Select(t => new TicketResponseModel
            //                {
            //                        Ticketid = t.Ticketid,
            //                        Ticketcode = t.Ticketcode,
            //                        Ticketpricecode = t.Ticketpricecode,
            //                        Isused = t.Isused,
            //                        Createdby = t.Createdby,
            //                        Createdat = t.Createdat,
            //                        Modifiedby = t.Modifiedby,
            //                        Modifiedat = t.Modifiedat,
            //                        Deleteflag = t.Deleteflag,
            //                        Ticketpriceid = t.TicketPrice!.Ticketpriceid,
            //                        Eventcode = t.TicketPrice.Eventcode,
            //                        Tickettypecode = t.TicketPrice.Tickettypecode,
            //                        Ticketprice = t.TicketPrice.Ticketprice,
            //                        Ticketquantity = t.TicketPrice.Ticketquantity,
            //                        Tickettypeid = t.TicketPrice.TicketType!.Tickettypeid,
            //                        Tickettypename = t.TicketPrice.TicketType.Tickettypename
            //                    })
            //                .ToListAsync();

            var tickets = await (from t in _db.TblTickets.AsNoTracking()
                                 join tp in _db.TblTicketprices on t.Ticketpricecode equals tp.Ticketpricecode
                                 join tt in _db.TblTickettypes on tp.Tickettypecode equals tt.Tickettypecode
                                 select new TicketResponseModel
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
                                     Ticketpriceid = tp.Ticketpriceid,
                                     Eventcode = tp.Eventcode,
                                     Tickettypecode = tp.Tickettypecode,
                                     Ticketprice = tp.Ticketprice,
                                     Ticketquantity = tp.Ticketquantity,
                                     Tickettypeid = tt.Tickettypeid,
                                     Tickettypename = tt.Tickettypename
                                 }
                                ).ToListAsync();


            return Result<List<TicketResponseModel>>.Success(tickets);
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            return Result<List<TicketResponseModel>>.SystemError(ex.Message);
        }
    }

    public async Task<Result<TicketResponseModel>> DeleteByCode(string tickedCode)
    {
        try
        {
            var data = await _db.TblTickets.FirstOrDefaultAsync(x => x.Ticketcode == tickedCode);
            if (data == null)
            {
                return Result<TicketResponseModel>.NotFoundError("No Data Found!");
            }

            var model = await _db.TblTickets
                .Where(x => x.Ticketcode == tickedCode && x.Deleteflag == false)
                .FirstOrDefaultAsync();
            model!.Deleteflag = true;

            _db.Entry(model).State = EntityState.Modified;
            var result = _db.SaveChanges();
            return Result<TicketResponseModel>.Success("Ticket is deleted successfully!");

        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            return Result<TicketResponseModel>.SystemError(ex.Message);
        }
    }

    public async Task<Result<TicketResponseModel>> UpdateTicket(string tickedCode, bool isUsed)
    {
        try
        {
            var data = await _db.TblTickets.FirstOrDefaultAsync(x => x.Ticketcode == tickedCode);
            if (data == null)
            {
                return Result<TicketResponseModel>.NotFoundError("No Data Found!");
            }

            if (isUsed) data!.Isused = isUsed;

            data!.Modifiedat = DateTime.Now;
            _db.Entry(data).State = EntityState.Modified;
            await _db.SaveAndDetachAsync();
            var model = new TicketResponseModel()
            {
                Isused = data.Isused,
                Modifiedat = data.Modifiedat,
            };
            return Result<TicketResponseModel>.Success("Ticket is updated successfully!");
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            return Result<TicketResponseModel>.SystemError(ex.Message);
        }
    }
}