using EventTicketingSystem.CSharp.Domain.Models.Features.TicketType;
using EventTicketingSystem.CSharp.Domain.Models.Features.VerificationCode;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTicketingSystem.CSharp.Domain.Features.TicketType;

public class DA_TicketType
{
    private readonly AppDbContext _appDbContext;
    private readonly ILogger _logger;
    private readonly CommonService _commonService;
    private const string CreatedByUserId = "Admin";
    private readonly string _connection;

    public DA_TicketType(AppDbContext appDbContext, ILogger<DA_TicketType> logger, IConfiguration configuration, CommonService commonService)
    {
        _appDbContext = appDbContext;
        _logger = logger;
        _connection = configuration.GetConnectionString("DbConnection")!;
        _commonService = commonService;
    }

    public async Task<Result<TicketTypeListResponseModel>> getTicketTypeListAsync()
    {
        var responseModel = new Result<TicketTypeListResponseModel>();
        var model = new TicketTypeListResponseModel();

        string query = @"SELECT 
            tt.tickettypecode,
            tt.eventcode,
            te.eventname,
            tt.tickettypename,
            tt.createdby,
            tt.createdat,
            tt.modifiedby,
            tt.modifiedat,
            tt.deleteflag,
            tt.tickettypeid,
            tp.ticketprice,
            tp.ticketquantity
        FROM tbl_tickettype tt
        LEFT JOIN tbl_ticketprice tp ON tt.tickettypecode = tp.tickettypecode
        LEFT JOIN tbl_event te ON te.eventcode = tt.eventcode
        WHERE tt.deleteflag = false";

        using IDbConnection dbConnection = new NpgsqlConnection(_connection);
        dbConnection.Open();

        var ticketTypeList = (await dbConnection.QueryAsync<TicketTypeListModel>(query)).ToList();
        model.TicketTypeList = ticketTypeList;

        responseModel = Result<TicketTypeListResponseModel>.Success(model, "Ticket types are retrieved successfullly!");
        return responseModel;
    }

    public async Task<Result<TicketTypeEditResponseModel>> getTicketTypeByCodeAsync(string code)
    {
        var responseModel = new Result<TicketTypeEditResponseModel>();
        var model = new TicketTypeEditResponseModel();

        string query = @"
    SELECT 
        tt.tickettypecode,
        tt.eventcode,
        te.eventname,
        tt.tickettypename,
        tt.createdby,
        tt.createdat,
        tt.modifiedby,
        tt.modifiedat,
        tt.deleteflag,
        tt.tickettypeid,
        tp.ticketprice,
        tp.ticketquantity
    FROM tbl_tickettype tt
    LEFT JOIN tbl_ticketprice tp ON tt.tickettypecode = tp.tickettypecode
    LEFT JOIN tbl_event te ON te.eventcode = tt.eventcode
    WHERE tt.deleteflag = false
      AND tt.tickettypecode = @TicketTypeCode";

        using IDbConnection dbConnection = new NpgsqlConnection(_connection);
        dbConnection.Open();

        var ticekttype = (await dbConnection.QueryAsync<TicketTypeEditModel>(query, new
        {
            TicketTypeCode = code
        })).FirstOrDefault();

        model.TicketType = ticekttype;

        responseModel = Result<TicketTypeEditResponseModel>.Success(model, "Ticket is retrieved successfullly!");
        return responseModel;
    }

    public async Task<Result<TicketTypeCreateResponseModel>> CreateTicketTypeAsync(TicketTypeCreateRequestModel requestModel)
    {
        var responseModel = new Result<TicketTypeCreateResponseModel>();
        if (string.IsNullOrEmpty(requestModel.TicketTypeName))
        {
            responseModel = Result<TicketTypeCreateResponseModel>.ValidationError("Ticket Type name cannot be Empty!");
            goto ReturnErrorResponse;
        }
        if (requestModel.Ticketprice <= 0)
        {
            responseModel = Result<TicketTypeCreateResponseModel>.ValidationError("Ticket Type price must be greater than 0!");
            goto ReturnErrorResponse;
        }
        if (requestModel.TicketQuantity <= 0)
        {
            responseModel = Result<TicketTypeCreateResponseModel>.ValidationError("Ticket Type quantity must be greater than 0!");
            goto ReturnErrorResponse;
        }

        int totalQty = await _appDbContext.TblEvents
            .Where(x => x.Eventcode == requestModel.EventCode && !x.Deleteflag)
            .Select(x => x.Totalticketquantity)
            .FirstOrDefaultAsync();

        int existingQty = await _appDbContext.TblTickettypes
            .Where(tt => tt.Eventcode == requestModel.EventCode && !tt.Deleteflag)
            .Join(_appDbContext.TblTicketprices.Where(tp => !tp.Deleteflag),
                tt => tt.Tickettypecode,
                tp => tp.Tickettypecode,
                (tt, tp) => tp.Ticketquantity)
            .SumAsync();

        int remainingQty = totalQty - existingQty;
        if (remainingQty - requestModel.TicketQuantity < 0)
        {
            responseModel = Result<TicketTypeCreateResponseModel>.ValidationError("The total number of tickets exceeds the allowed quantity for this event.");
            goto ReturnErrorResponse;
        }

        try
        {
            var TblTickettype = new TblTickettype
            {
                Tickettypeid = GenerateUlid(),
                Tickettypecode = await _commonService.GenerateSequenceCode(EnumTableUniqueName.Tbl_TicketType),
                Tickettypename = requestModel.TicketTypeName,
                Eventcode = requestModel.EventCode,
                Createdat = DateTime.Now,
                Createdby = CreatedByUserId,
                Deleteflag = false,
            };

            await _appDbContext.TblTickettypes.AddAsync(TblTickettype);
            await _appDbContext.SaveChangesAsync();

            var ticketprice = new TblTicketprice
            {
                Ticketpriceid = GenerateUlid(),
                Ticketpricecode = await _commonService.GenerateSequenceCode(EnumTableUniqueName.Tbl_TicketPrice),
                Ticketprice = requestModel.Ticketprice,
                Ticketquantity = requestModel.TicketQuantity,
                Tickettypecode = TblTickettype.Tickettypecode,
                Createdat = DateTime.Now,
                Createdby = CreatedByUserId,
                Deleteflag = false,
            };

            await _appDbContext.TblTicketprices.AddAsync(ticketprice);
            await _appDbContext.SaveChangesAsync();


            var ticketList = new List<TblTicket>();
            for (int i = 0; i < requestModel.TicketQuantity; i++)
            {
                var ticket = new TblTicket
                {
                    Ticketid = GenerateUlid(),
                    Ticketcode = await _commonService.GenerateSequenceCode(EnumTableUniqueName.Tbl_Ticket),
                    Isused = false,
                    Ticketpricecode = ticketprice.Ticketpricecode,
                    Createdat = DateTime.Now,
                    Createdby = CreatedByUserId,
                    Deleteflag = false
                };
                ticketList.Add(ticket);
            }

            await _appDbContext.TblTickets.AddRangeAsync(ticketList);
            await _appDbContext.SaveChangesAsync();

            responseModel = Result<TicketTypeCreateResponseModel>.Success("Ticket Type and tickets are created successfully!");
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            responseModel = Result<TicketTypeCreateResponseModel>.SystemError(ex.ToString());
        }

        return responseModel;

    ReturnErrorResponse:
        return responseModel;

    }

    public async Task<Result<TicketTypeUpdateResponseModel>> UpdateTicketTypeAsync(TicketTypeUpdateRequestModel requestModel)
    {
        var responseModel = new Result<TicketTypeUpdateResponseModel>();

        if (string.IsNullOrEmpty(requestModel.TicketTypeCode))
        {
            return Result<TicketTypeUpdateResponseModel>.ValidationError("There is no ticket type code to update!");
        }

        if (string.IsNullOrEmpty(requestModel.TicketTypeName))
        {
            return Result<TicketTypeUpdateResponseModel>.ValidationError("Please enter the Ticket type name to update.");
        }

        var tickettype = await _appDbContext.TblTickettypes
            .Where(x => !x.Deleteflag)
            .FirstOrDefaultAsync(x => x.Tickettypecode == requestModel.TicketTypeCode);

        if (tickettype == null)
        {
            return Result<TicketTypeUpdateResponseModel>.NotFoundError("There is no ticket type with this code!");
        }

        try
        {
            tickettype.Tickettypename = requestModel.TicketTypeName;
            tickettype.Modifiedat = DateTime.Now;
            tickettype.Modifiedby = CreatedByUserId;
            _appDbContext.Entry(tickettype).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();

            responseModel = Result<TicketTypeUpdateResponseModel>.Success("Ticket type is updated successfully!");
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            responseModel = Result<TicketTypeUpdateResponseModel>.SystemError(ex.ToString());
        }

        return responseModel;
    }

    public async Task<Result<TicketTypeDeleteResponseModel>> DeleteTicketTypeAsync(string code)
    {
        var responseModel = new Result<TicketTypeDeleteResponseModel>();


        if (string.IsNullOrEmpty(code))
        {
            return Result<TicketTypeDeleteResponseModel>.UserInputError("There is no ticket type code to delete!");
        }

        var tickettype = await _appDbContext.TblTickettypes
        .Where(x => !x.Deleteflag)
            .FirstOrDefaultAsync(x => x.Tickettypecode == code);

        if (tickettype == null)
        {
            return Result<TicketTypeDeleteResponseModel>.NotFoundError("There is no ticket type with this code!");
        }

        var ticketprice = await _appDbContext.TblTicketprices
        .Where(x => !x.Deleteflag)
            .FirstOrDefaultAsync(x => x.Tickettypecode == code);

        var tickets = await _appDbContext.TblTickets
        .Where(x => !x.Deleteflag && x.Ticketpricecode == ticketprice.Ticketpricecode)
            .ToListAsync();

        try
        {
            tickettype.Deleteflag = true;
            tickettype.Modifiedat = DateTime.Now;
            tickettype.Modifiedby = CreatedByUserId;
            _appDbContext.Entry(tickettype).State = EntityState.Modified;

            if(ticketprice != null)
            {
                ticketprice.Deleteflag = true;
                ticketprice.Modifiedat = DateTime.Now;
                ticketprice.Modifiedby = CreatedByUserId;
                _appDbContext.Entry(ticketprice).State = EntityState.Modified;

                if (tickets.Count > 0)
                {
                    foreach (var ticket in tickets)
                    {
                        ticket.Deleteflag = true;
                        ticket.Modifiedat = DateTime.Now;
                        ticket.Modifiedby = CreatedByUserId;
                        _appDbContext.Entry(ticket).State = EntityState.Modified;
                    }
                }
            }

            await _appDbContext.SaveChangesAsync();

            responseModel = Result<TicketTypeDeleteResponseModel>.Success("Ticket type is deleted successfully!");
        }
        catch (Exception ex)
        {
            _logger.LogExceptionError(ex);
            responseModel = Result<TicketTypeDeleteResponseModel>.SystemError(ex.ToString());
        }

        return responseModel;
    }
}
