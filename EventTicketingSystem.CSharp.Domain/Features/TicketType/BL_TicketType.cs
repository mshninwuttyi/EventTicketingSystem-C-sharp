using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTicketingSystem.CSharp.Domain.Features.TicketType
{
    public class BL_TicketType
    {
        public readonly DA_TicketType _daService;

        public BL_TicketType(DA_TicketType service)
        {
            _daService = service;
        }

        public async Task<Result<TicketTypeListResponseModel>> List()
        {
            return await _daService.ListAsync();
        }

        public async Task<Result<TicketTypeEditResponseModel>> Edit(string ticketTypeCode)
        {
            return await _daService.Edit(ticketTypeCode);
        }

        public async Task<Result<TicketTypeCreateResponseModel>> Create(TicketTypeCreateRequestModel requestModel)
        {
            return await _daService.Create(requestModel);
        }

        public async Task<Result<TicketTypeUpdateResponseModel>> Update(TicketTypeUpdateRequestModel requestModel)
        {
            return await _daService.Update(requestModel);   
        }

        public async Task<Result<TicketTypeDeleteResponseModel>> Delete(string ticketTypeCode)
        {
            return await _daService.Delete(ticketTypeCode);
        }
}
