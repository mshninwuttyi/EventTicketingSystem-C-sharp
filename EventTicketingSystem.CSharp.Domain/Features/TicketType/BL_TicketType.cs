using EventTicketingSystem.CSharp.Domain.Models.Features.TicketType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTicketingSystem.CSharp.Domain.Features.TicketType;

public class BL_TicketType
{
    private readonly DA_TicketType _da_ticketType;

    public BL_TicketType(DA_TicketType da_ticketType)
    {
        _da_ticketType = da_ticketType;
    }

    public async Task<Result<TicketTypeListResponseModel>> getTicketTypeList()
    {
        var lst = await _da_ticketType.getTicketTypeListAsync();
        return lst;
    }

    public async Task<Result<TicketTypeEditResponseModel>> getTicketTypeByCode(string code)
    {
        var ticketType = await _da_ticketType.getTicketTypeByCodeAsync(code);
        return ticketType;
    }

    public async Task<Result<TicketTypeCreateResponseModel>> createTicketType (TicketTypeCreateRequestModel requestModel)
    {
        var result = await _da_ticketType.CreateTicketTypeAsync(requestModel);
        return result;
    }

    public async Task<Result<TicketTypeUpdateResponseModel>> updateTicketType(TicketTypeUpdateRequestModel requestModel)
    {
        var result = await _da_ticketType.UpdateTicketTypeAsync(requestModel);
        return result;
    }

    public async Task<Result<TicketTypeDeleteResponseModel>> deleteTickeType(string code)
    {
        var result = await _da_ticketType.DeleteTicketTypeAsync(code);
        return result;
    }
}
